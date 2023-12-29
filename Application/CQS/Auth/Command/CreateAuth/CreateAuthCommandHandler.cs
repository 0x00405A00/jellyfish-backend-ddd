using Application.Abstractions.Messaging;
using Domain.Const;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Microsoft.Extensions.Configuration;
using Shared.Authentification.Jwt;
using Shared.Const;
using Shared.DataTransferObject;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Entities.User;
using Newtonsoft.Json.Linq;
using System;

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
            var user = await _userRepository.GetAsync(user => user.Email.EmailValue == request.Email && user.Password == request.Password);
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
            var isAdmin = user.UserRoles.Where(x => x.Role.Id == new Domain.Entities.Role.RoleId(RoleConst.AdminRoleUuid)).Any();
            var isRoot = user.UserRoles.Where(x => x.Role.Id == new Domain.Entities.Role.RoleId(RoleConst.RootRoleUuid)).Any();
            var isUser = user.ActivationDateTime != null && user.ActivationDateTime != DateTime.MinValue;
            Claim[] claims = new Claim[] {
                new Claim(AuthorizationConst.Claims.ClaimTypeIsAdmin, isAdmin.ToString()),
                new Claim(AuthorizationConst.Claims.ClaimTypeIsRoot, isRoot.ToString()),
                new Claim(AuthorizationConst.Claims.ClaimTypeIsActivatedUser, isUser.ToString()),
                new Claim(AuthorizationConst.Claims.ClaimTypeUserEmail, user.Email.ToString()),
                new Claim(AuthorizationConst.Claims.ClaimTypeUserUuid, user.Id.ToString()),
            };
            var token = JwtHandler.EncodeJwt(symetricKey, issuer, audience, claims, texpMinTimeSpan);

            var refreshToken = JwtHandler.EncodeJwt(symetricKey, issuer, audience, claims, rexpMinTimeSpan);
            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshTokenStr = new JwtSecurityTokenHandler().WriteToken(refreshToken);
            try
            {
                var auth = Domain.Entities.Auths.Auth.Create(Domain.Entities.Auths.Auth.NewId(),
                    user.Id,
                    request.LocalIp.ToString(),
                    request.LocalIpPort,
                    request.RemoteIp.ToString(),
                    request.RemoteIpPort,
                    tokenStr,
                    DateTime.Now.Add(texpMinTimeSpan),
                    request.UserAgent,
                    refreshTokenStr,
                    DateTime.Now.Add(rexpMinTimeSpan),
                    DateTime.Now,
                    user);
                
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
