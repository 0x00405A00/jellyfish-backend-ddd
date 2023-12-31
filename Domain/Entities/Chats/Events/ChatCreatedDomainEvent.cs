using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Chats.Events
{
    public record ChatCreatedDomainEvent(ChatId ChatId) : DomainEvent(ChatId)
    {
    }
}
