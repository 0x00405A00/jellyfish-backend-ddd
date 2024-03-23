using Application.Abstractions.Messaging;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.Chat.Command.AcknowledgeMessageDeliveryByChat
{
    public record AcknowledgeMessageDeliveryByChatCommand(Guid UserId,Guid MessageId) : ICommand<MessageDTO>
    {
    }
}
