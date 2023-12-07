using Application.Abstractions.Messaging;

namespace Application.CQS.Messenger.Chat.Command.RevokeChatAdmin
{
    /// <summary>
    /// Revoke a chat member the admin rights
    /// </summary>
    /// <param name="ActorId">The user that is executing the command (must be chat admin)</param>
    /// <param name="ChatId">The chat</param>
    /// <param name="UserId">The target user that should have removed the admin rights</param>
    public record RevokeChatAdminCommand(Guid ActorId,Guid ChatId,Guid UserId) : ICommand<bool>
    {
    }
}
