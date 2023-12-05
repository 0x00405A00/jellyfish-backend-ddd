using Application.Abstractions.Messaging;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.Chat.Command.UpdateMessage
{
    public record UpdateMessageCommand(Guid ModifiedById,Guid ChatId,Guid MessageId, MessageDTO messageDTOs) : ICommand<MessageDTO>;
}
