using Domain.Primitives;

namespace Domain.Entities.Chats.Events
{
    public record ChatUpdatedDomainEvent(Chat e) : DomainEvent(e)
    {
    }
}
