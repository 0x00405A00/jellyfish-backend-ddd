using Application.Abstractions.Messaging;
using Shared.DataTransferObject.Messenger;

namespace Application.CQS.Messenger.Chat.Queries.GetNotDeliveredMessagesByChat
{
    public record GetNotDeliveredMessagesByChatQuery(Guid UserId,Guid ChatId) : IQuery<ICollection<MessageDTO>>
    {
    }
}
