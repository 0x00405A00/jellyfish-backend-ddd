using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Chats.Events
{
    public record ChatUpdatedDomainEvent(ChatId ChatId) : DomainEvent(ChatId)
    {
    }
}
