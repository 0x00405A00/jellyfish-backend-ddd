using Application.Abstractions.Messaging;

namespace Application.CQS.Messenger.Chat.Command.DeleteMessage
{
    public record DeleteMessageCommand(Guid DeletedById, Guid ChatId, Guid MessageId) : ICommand<Guid>;
}
