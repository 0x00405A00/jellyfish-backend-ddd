using Application.Abstractions.Messaging;
using Application.CQS.User.Commands.CreateUser;
using Domain.Extension;
using Domain.ValueObjects;
using MediatR;
using Shared.DataTransferObject;

namespace Application.CQS.User.Commands.RegisterUser.Register
{
    internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, UserDTO>
    {
        private readonly ISender _sender;

        public RegisterUserCommandHandler(
            ISender sender)
        {
            this._sender = sender;
        }

        public async Task<Result<UserDTO>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var systemUser = Domain.Entities.Users.User.GetSystemUser();
            var command = new CreateUserCommand(
                systemUser.Id.ToGuid(),
                request.UserName,
                request.Password,
                request.PasswordRepeat,
                request.FirstName,
                request.LastName,
                request.Email,
                request.Phone,
                request.DateOfBirth);
            var result = await _sender.Send(command);
            return result;

        }

    }
}
