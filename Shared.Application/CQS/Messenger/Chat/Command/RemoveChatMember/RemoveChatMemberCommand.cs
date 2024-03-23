using Application.Abstractions.Messaging;

namespace Application.CQS.Messenger.Chat.Command.RemoveChatMember
{
    /// <summary>
    /// Remove a user from chat
    /// </summary>
    /// <param name="ActorId">The user that is executing the command (must be chat admin)</param>
    /// <param name="ChatId">The chat</param>
    /// <param name="UserId">The target user that should be removed from chat</param>
    public record RemoveChatMemberCommand(Guid ActorId, Guid ChatId, Guid UserId) : ICommand<bool>
    {
    }
}
