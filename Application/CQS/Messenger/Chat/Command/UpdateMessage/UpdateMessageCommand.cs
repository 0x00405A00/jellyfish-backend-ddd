using Application.Abstractions.Messaging;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.Chat.Command.UpdateMessage
{
    public record UpdateMessageCommand() : ICommand<ChatDTO>;
}
