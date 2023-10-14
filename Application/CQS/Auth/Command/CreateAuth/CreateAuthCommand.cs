using Application.Abstractions.Messaging;
using Application.DataTransferObject;

namespace Application.CQS.Auth.Command.CreateAuth
{
    /// <summary>
    /// Create Auth Command Record
    /// </summary>
    /// <param name="Email">Users email</param>
    /// <param name="Password">User Password</param>
    public record CreateAuthCommand(string Email,
                                    string Password) : ICommand<AuthDTO>;
}
