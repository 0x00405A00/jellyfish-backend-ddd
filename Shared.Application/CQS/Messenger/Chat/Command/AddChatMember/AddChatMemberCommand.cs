using Application.Abstractions.Messaging;

namespace Application.CQS.Messenger.Chat.Command.AddChatMember
{
    /// <summary>
    /// Adds a user to chat
    /// </summary>
    /// <param name="ActorId">The user that is executing the command (must be chat admin)</param>
    /// <param name="ChatId">The chat</param>
    /// <param name="UserId">The target user that should be added from chat</param>
    public record AddChatMemberCommand(Guid ActorId, Guid ChatId, Guid UserId) : ICommand<bool>
    {
    }
}
