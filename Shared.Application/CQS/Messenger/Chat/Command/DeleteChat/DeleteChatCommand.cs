using Application.Abstractions.Messaging;

namespace Application.CQS.Messenger.Chat.Command.DeleteChat
{
    public record DeleteChatCommand(Guid DeletedByUserId,Guid ChatId) : ICommand<Guid>;
}
