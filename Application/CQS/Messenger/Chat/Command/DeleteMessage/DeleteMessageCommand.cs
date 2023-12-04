using Application.Abstractions.Messaging;

namespace Application.CQS.Messenger.Chat.Command.DeleteMessage
{
    public record DeleteMessageCommand() : ICommand<Guid>;
}
