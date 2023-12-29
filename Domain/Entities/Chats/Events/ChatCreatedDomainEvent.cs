using Domain.Primitives;

namespace Domain.Entities.Chats.Events
{
    public record ChatCreatedDomainEvent(Chat e) : DomainEvent(e)
    {
    }
}
