using Domain.Primitives;

namespace Domain.Entities.Messages.Events
{
    public record MessageCreatedDomainEvent(Message e) : DomainEvent(e)
    {
    }
}
