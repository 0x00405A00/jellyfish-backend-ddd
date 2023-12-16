using Application.Abstractions.Messaging;
using Application.CQS.Auth.Command.CreateAuth;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObject;

namespace Application.CQS.Auth.Command.RefreshAuth
{
    internal sealed class RefreshAuthCommandHandler : ICommandHandler<RefreshAuthCommand, AuthDTO>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        public RefreshAuthCommandHandler(
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


        public async Task<Result<AuthDTO>> Handle(RefreshAuthCommand request, CancellationToken cancellationToken)
        {
            if ((request.Token == null || String.IsNullOrEmpty(request.Token))&& request.RefreshToken == null || String.IsNullOrEmpty(request.RefreshToken))
            {
                return Result<AuthDTO>.Failure("session invalid not found");
            }
            var auth = await _authRepository.GetAsync(a=> a.RefreshToken == request.RefreshToken);
            if (auth == null)
            {
                return Result<AuthDTO>.Failure("session not found");
            }
            var config = _configuration.GetSection("Infrastructure:Authentification:Jwt");
            var texpMin = int.Parse(config["TokenExpiresMinutes"]);
            var rexpMin = int.Parse(config["RefreshTokenExpiresMinutes"]);
            var texpMinTimeSpan = new TimeSpan(0, texpMin, 0);
            var rexpMinTimeSpan = new TimeSpan(0, rexpMin, 0);
            try
            {
                auth.Refresh(texpMinTimeSpan, rexpMinTimeSpan); 
                _authRepository.Update(auth);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Result<AuthDTO>.Failure(ex.Message);
            }
            var authDto = new AuthDTO {
                Token = auth.Token,
                RefreshToken = auth.RefreshToken,
                TokenExpiresIn = auth.TokenExpiresIn,
                RefreshTokenExpiresIn = auth.RefreshTokenExpiresIn,
            };
            return Result<AuthDTO>.Success(authDto);
        }
    }
}
