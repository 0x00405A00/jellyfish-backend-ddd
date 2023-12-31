using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Chats.Events
{
    public record ChatDeletedDomainEvent(ChatId ChatId) : DomainEvent(ChatId)
    {
    }
}
