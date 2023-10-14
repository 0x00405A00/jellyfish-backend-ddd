using Application.Abstractions.Messaging;
using Application.DataTransferObject;

namespace Application.CQS.User.Commands.PasswordReset.Request
{
    public class UserPasswordResetRequestCommand : ICommand<bool>
    {
        public string Email { get; }

        public UserPasswordResetRequestCommand(string Email)
        {
            this.Email = Email;
        }

    }
}