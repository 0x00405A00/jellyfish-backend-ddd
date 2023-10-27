using Application.Abstractions.Messaging;

namespace Application.CQS.User.Commands.PasswordReset.Reset
{
    public class UserPasswordResetCommand : ICommand<bool>
    {
        public string PasswordResetBase64Token { get; }
        public string Password { get; }
        public string PasswordConfirm { get; }
        public string PasswordResetCode { get; }

        public UserPasswordResetCommand(string Password,string PasswordConfirm,string PasswordResetCode, string PasswordResetBase64Token)
        {
            this.PasswordResetBase64Token = PasswordResetBase64Token;
            this.Password = Password;
            this.PasswordConfirm = PasswordConfirm;
            this.PasswordResetCode = PasswordResetCode;
        }
    }
}
