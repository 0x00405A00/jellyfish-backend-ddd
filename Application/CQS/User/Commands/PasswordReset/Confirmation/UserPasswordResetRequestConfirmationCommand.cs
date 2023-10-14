using Application.Abstractions.Messaging;

namespace Application.CQS.User.Commands.PasswordReset.Confirmation
{
    public class UserPasswordResetRequestConfirmationCommand :ICommand<bool>
    {
        public string PasswordResetCode { get; }

        public UserPasswordResetRequestConfirmationCommand(string PasswordResetCode)
        {
            this.PasswordResetCode = PasswordResetCode;
        }
    }
}
