using Application.Abstractions.Messaging;

namespace Application.CQS.User.Commands.PasswordReset.Reset
{
    public record UserPasswordResetCommand(string Password, string PasswordConfirm, string PasswordResetCode, string PasswordResetBase64Token) : ICommand<bool>
    {
    }
}
