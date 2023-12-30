using Application.Abstractions.Messaging;
using Domain.Entities.Users.Exceptions;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using MediatR;

namespace Application.CQS.User.Commands.UpdatePassword
{
    internal sealed class UpdateUserPasswordCommandHandler : ICommandHandler<UpdateUserPasswordCommand, Guid>
    {
        private readonly IMediator mediator;
        private readonly IUserRepository _userRepository;

        public UpdateUserPasswordCommandHandler(
            IMediator mediator,
            IUserRepository userRepository)
        {
            this.mediator = mediator;
            _userRepository = userRepository;
        }

        public async Task<Result<Guid>> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == null)
            {
                return Result<Guid>.Failure("user not found");
            }
            var user = await _userRepository.GetAsync(user => user.Id == request.UserId.ToIdentification<UserId>());
            if (user == null)
            {
                return Result<Guid>.Failure("user not found");
            }

            try
            {
                user.UpdatePassword(user, request.Password, request.PasswordConfirm);
            }
            catch (InvalidPasswordException ex)
            {
                return Result<Guid>.Failure(ex.Message);
            }

            _userRepository.Update(user);
            _userRepository.PublishDomainEvents(user, mediator);
            return Result<Guid>.Success(user.Id.ToGuid());
        }
    }
}
