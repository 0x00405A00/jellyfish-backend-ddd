using Application.Abstractions.Messaging;
using Domain.Entities.User.Exception;
using Domain.Exceptions;
using Domain.ValueObjects;
using Infrastructure.Abstractions;

namespace Application.CQS.User.Commands.DeleteUser
{
    internal sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteUserCommandHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(user => user.Uuid == request.UserId);
            if (user == null)
                return Result<Guid>.Failure("target user not found");
            var deletetByUser = await _userRepository.GetAsync(user => user.Uuid == request.DeletedByUserId);
            if (deletetByUser == null)
                return Result<Guid>.Failure("execution user not found");
            try
            {
                user.Remove(deletetByUser);
            }
            catch(Exception ex)
            {
                return Result<Guid>.Failure(ex.Message);
            }
            _userRepository.Remove(user);

            return Result<Guid>.Success();
        }
    }
}
