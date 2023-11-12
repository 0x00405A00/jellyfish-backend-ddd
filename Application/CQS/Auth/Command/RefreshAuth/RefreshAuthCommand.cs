using Application.Abstractions.Messaging;
using Shared.DataTransferObject;

namespace Application.CQS.Auth.Command.RefreshAuth
{
    /// <summary>
    /// Refresh Login
    /// </summary>
    /// <param name="token"></param>
    /// <param name="refreshToken"></param>
    public record RefreshAuthCommand(string Token,
                                    string RefreshToken) : ICommand<AuthDTO>;
}
