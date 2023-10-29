using Application.Abstractions.Messaging;
using Shared.DataTransferObject;
using System.Net;

namespace Application.CQS.Auth.Command.CreateAuth
{
    /// <summary>
    /// Create Auth Command Record
    /// </summary>
    /// <param name="Email">Users email</param>
    /// <param name="Password">User Password</param>
    public record CreateAuthCommand(string Email,
                                    string Password,
                                    IPAddress LocalIp,
                                    int LocalIpPort,
                                    IPAddress RemoteIp,
                                    int RemoteIpPort,
                                    string UserAgent) : ICommand<AuthDTO>;
}
