using Application.Abstractions.Messaging;
using Domain.Exceptions;
using Domain.ValueObjects;
using Infrastructure.Abstractions;

namespace Application.CQS.Auth.Command.RemoveAuth
{
    internal sealed class RemoveAuthCommandHandler : ICommandHandler<RemoveAuthCommand, Guid>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RemoveAuthCommandHandler(
            IAuthRepository authRepository,
            IUnitOfWork unitOfWork)
        {
            _authRepository = authRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(RemoveAuthCommand request, CancellationToken cancellationToken)
        {
            if (request.JwtBearer == null)
            {
                throw new InvalidOperationException($"jwtBearer is {request.JwtBearer}");
            }
            var auth = await _authRepository.GetAsync(auth => auth.Token == request.JwtBearer);
            if (auth == null)
                throw new AuthentificationFailedException();

            _authRepository.Remove(auth);

            return Result<Guid>.Success(auth.Id.Id);
        }
    }
}
