using Application.Abstractions.Messaging;

namespace Application.CQS.User.Commands.PasswordReset.Reset
{
    public class UserPasswordResetCommand : ICommand<bool>
    {
        public string PasswordResetBase64Token { get; }
        public string Password { get; }
        public string PasswordResetCode { get; }

        public UserPasswordResetCommand(string PasswordResetBase64Token)
        {
            this.PasswordResetBase64Token = PasswordResetBase64Token;
        }

        public UserPasswordResetCommand(string Password, string PasswordResetCode)
        {
            this.Password = Password;
            this.PasswordResetCode = PasswordResetCode;
        }
    }
}
