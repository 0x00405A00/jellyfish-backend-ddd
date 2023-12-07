using Application.Abstractions.Messaging;

namespace Application.CQS.Messenger.Chat.Command.AssignChatAdmin
{
    /// <summary>
    /// Assign chat member the admin access rights
    /// </summary>
    /// <param name="ActorId">The user that is executing the command (must be chat admin)</param>
    /// <param name="ChatId">The chat</param>
    /// <param name="UserId">The target user that should have the admin rights</param>
    public record AssignChatAdminCommand(Guid ActorId, Guid ChatId, Guid UserId) : ICommand<bool>
    {
    }
}
