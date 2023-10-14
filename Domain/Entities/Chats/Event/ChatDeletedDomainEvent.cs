using Domain.Primitives;

namespace Domain.Entities.Chats.Event
{
    public record ChatDeletedDomainEvent(Chat e) : DomainEvent(e)
    {
    }
}
