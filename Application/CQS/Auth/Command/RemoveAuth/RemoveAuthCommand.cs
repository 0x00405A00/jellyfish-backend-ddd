using Application.Abstractions.Messaging;

namespace Application.CQS.Auth.Command.RemoveAuth
{
    /// <summary>
    /// Remove Auth Command for Logout
    /// </summary>
    /// <param name="JwtBearer">Jwt Bearer Token from Authorization Header from Http Request</param>
    public record RemoveAuthCommand(string JwtBearer) : ICommand<Guid>;
}
