using Application.Abstractions.Messaging;
using Domain.Entities.User.Exception;
using Domain.Exceptions;
using Domain.Primitives;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;

namespace Application.CQS.User.Commands.DeleteUser
{
    internal sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, Guid>
    {
        private readonly ISender sender;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteUserCommandHandler(
            ISender sender,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            this.sender = sender;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(user => user.Uuid == request.UserId && user.DeletedTime==null);
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
            try
            {
                _userRepository.Update(user);
                await _unitOfWork.SaveChangesAsync();
            }
            catch(Exception ex) {
                return Result<Guid>.Failure(ex.Message);
            }

            return Result<Guid>.Success(user.Uuid.ToGuid());
        }
    }
}
