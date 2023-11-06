using Application.Abstractions.Messaging;
using Domain.Const;
using Domain.Exceptions;
using Domain.Primitives;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.Authentification;
using Microsoft.Extensions.Configuration;
using Shared.Authentification.Jwt;
using Shared.Const;
using Shared.DataTransferObject;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.CQS.Auth.Command.CreateAuth
{
    internal sealed class CreateAuthCommandHandler : ICommandHandler<CreateAuthCommand, AuthDTO>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        public CreateAuthCommandHandler(
            IConfiguration configuration,
            IUserRepository userRepository,
            IAuthRepository authRepository,
            IUnitOfWork unitOfWork)
        {
            this._configuration = configuration;
            _userRepository = userRepository;
            _authRepository = authRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<AuthDTO>> Handle(CreateAuthCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(user => user.Email == request.Email && user.Password == request.Password);
            if(user == null) {
                return Result<AuthDTO>.Failure("username or password wrong");
            }

            var config = _configuration.GetSection("Infrastructure:Authentification:Jwt");
            var symetricKey = config["SymetricKey"];
            var issuer = config["Issuer"];
            var audience = config["Audience"];
            var texpMin = int.Parse(config["TokenExpiresMinutes"]);
            var rexpMin = int.Parse(config["RefreshTokenExpiresMinutes"]);
            var texpMinTimeSpan = new TimeSpan(0, texpMin, 0);
            var rexpMinTimeSpan = new TimeSpan(0, rexpMin, 0);
            var isAdmin = user.Roles.Where(x => x.Role.Uuid == new Domain.Entities.Role.RoleId(RoleConst.AdminRoleUuid)) != null;
            var isRoot = user.Roles.Where(x => x.Role.Uuid == new Domain.Entities.Role.RoleId(RoleConst.RootRoleUuid)) != null;
            var isUser = user.ActivationDateTime != null && user.ActivationDateTime != DateTime.MinValue;
            Claim[] claims = new Claim[] {
                new Claim(AuthorizationConst.Claims.ClaimTypeIsAdmin, isAdmin.ToString()),
                new Claim(AuthorizationConst.Claims.ClaimTypeIsRoot, isRoot.ToString()),
                new Claim(AuthorizationConst.Claims.ClaimTypeIsActivatedUser, isUser.ToString()),
                new Claim(AuthorizationConst.Claims.ClaimTypeUserEmail, user.Email.ToString()),
                new Claim(AuthorizationConst.Claims.ClaimTypeUserUuid, user.Uuid.ToString()),
            };
            var token = JwtHandler.EncodeJwt(symetricKey, issuer, audience, claims, texpMinTimeSpan);

            var refreshToken = JwtHandler.EncodeJwt(symetricKey, issuer, audience, claims, rexpMinTimeSpan);
            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshTokenStr = new JwtSecurityTokenHandler().WriteToken(refreshToken);
            try
            {
                var auth = new Infrastructure.DatabaseEntity.Auth
                {
                    Uuid = Guid.NewGuid(),
                    UserUuid = user.Uuid.ToGuid(),
                    CreatedTime =DateTime.Now,
                    Token =tokenStr,
                    TokenExpiresIn =DateTime.Now.Add(texpMinTimeSpan),
                    RefreshToken = refreshTokenStr,
                    RefreshTokenExpiresIn = DateTime.Now.Add(rexpMinTimeSpan),
                    IpAddrLocal =request.LocalIp.ToString(),
                    LocalPort = request.LocalIpPort,
                    IpAddrRemote = request.RemoteIp.ToString(),
                    RemotePort = request.RemoteIpPort,
                    UserAgent =request.UserAgent,
                };
                _authRepository.Add(auth);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result<AuthDTO>.Failure(ex.Message);
            }
            var authDto = new AuthDTO {
                Token = tokenStr,
                RefreshToken = refreshTokenStr,
                TokenExpiresIn = DateTimeExtension.DateTimeExtension.UnixTimeStampToDateTime((long)token.Payload.Exp),
                RefreshTokenExpiresIn = DateTimeExtension.DateTimeExtension.UnixTimeStampToDateTime((long)refreshToken.Payload.Exp),
            };
            return Result<AuthDTO>.Success(authDto);
        }
    }
}
