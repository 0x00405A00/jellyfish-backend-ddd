using Domain.Primitives;

namespace Domain.Entities.Chats.Event
{
    public record ChatCreatedDomainEvent(Chat e) : DomainEvent(e)
    {
    }
}
