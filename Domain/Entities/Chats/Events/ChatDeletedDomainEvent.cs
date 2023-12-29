using Domain.Primitives;

namespace Domain.Entities.Chats.Events
{
    public record ChatDeletedDomainEvent(Chat e) : DomainEvent(e)
    {
    }
}
