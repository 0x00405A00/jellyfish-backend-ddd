using Domain.Primitives;

namespace Domain.Entities.Chats.Event
{
    public record ChatUpdatedDomainEvent(Chat e) : DomainEvent(e)
    {
    }
}
