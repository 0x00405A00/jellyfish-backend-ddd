using Application.Abstractions.Messaging;
using Application.DataTransferObject;
using Domain.Const;
using Domain.Exceptions;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.Authentification;
using Microsoft.Extensions.Configuration;
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
                throw new AuthentificationFailedException();
            }

            var config = _configuration.GetSection("Infrastructure:Authentification:Jwt");
            var symetricKey = config["SymetricKey"];
            var issuer = config["Issuer"];
            var audience = config["Audience"];
            var texpMin = int.Parse(config["TokenExpiresMinutes"]);
            var rexpMin = int.Parse(config["RefreshTokenExpiresMinutes"]);
            var isAdmin = user.Roles.Where(x => x.Role.Uuid == new Domain.Entities.Role.RoleId(AuthorizationConst.Role.AdminRoleUuid)) != null;
            var isRoot = user.Roles.Where(x => x.Role.Uuid == new Domain.Entities.Role.RoleId(AuthorizationConst.Role.RootRoleUuid)) != null;
            var isUser = user.ActivationDateTime != null && user.ActivationDateTime != DateTime.MinValue;
            Claim[] claims = new Claim[] {
                new Claim(AuthorizationConst.Claims.ClaimTypeIsAdmin, isAdmin.ToString()),
                new Claim(AuthorizationConst.Claims.ClaimTypeIsRoot, isRoot.ToString()),
                new Claim(AuthorizationConst.Claims.ClaimTypeIsActivatedUser, isUser.ToString()),
                new Claim(AuthorizationConst.Claims.ClaimTypeUserEmail, user.Email.ToString()),
                new Claim(AuthorizationConst.Claims.ClaimTypeUserUuid, user.Uuid.ToString()),
            };
            var token = JwtHandler.EncodeJwt(symetricKey, issuer, audience, claims,new TimeSpan(0,texpMin,0));

            var refreshToken = JwtHandler.EncodeJwt(symetricKey, issuer, audience, claims, new TimeSpan(0, rexpMin, 0));
            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshTokenStr = new JwtSecurityTokenHandler().WriteToken(refreshToken);

            var result = Result<AuthDTO>.Success();
            result.Value = new AuthDTO {
                Token = tokenStr,
                RefreshToken = refreshTokenStr,
                TokenExpiresIn = DateTimeExtension.DateTimeExtension.UnixTimeStampToDateTime((long)token.Payload.Exp),
                RefreshTokenExpiresIn = DateTimeExtension.DateTimeExtension.UnixTimeStampToDateTime((long)refreshToken.Payload.Exp),
            };
            return result;
        }
    }
}
