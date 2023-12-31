using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Chats.Events
{
    public record ChatAppendMessageDomainEvent(ChatId ChatId, UserId MessageOwnerUserId, MessageId MessageId) : DomainEvent(ChatId)
    {
    }
}
