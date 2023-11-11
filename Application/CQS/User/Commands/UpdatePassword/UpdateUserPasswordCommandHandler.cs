using Application.Abstractions.Messaging;
using Domain.Entities.User.Exception;
using Domain.Exceptions;
using Domain.Primitives;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using System.Diagnostics.CodeAnalysis;

namespace Application.CQS.User.Commands.UpdatePassword
{
    internal sealed class UpdateUserPasswordCommandHandler : ICommandHandler<UpdateUserPasswordCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateUserPasswordCommandHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == null)
            {
                throw new InvalidOperationException($"userId is {request.UserId}");
            }
            var user = await _userRepository.GetAsync(user => user.Uuid == request.UserId);
            if (user == null)
                throw new UserNotFoundException(request.UserId);

            try
            {
                user.UpdatePassword(user, request.Password, request.PasswordConfirm);
            }
            catch (InvalidPasswordException ex)
            {
                return Result<Guid>.Failure(ex.Message);
            }

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
            return Result<Guid>.Success(user.Uuid.ToGuid());
        }
    }
}
