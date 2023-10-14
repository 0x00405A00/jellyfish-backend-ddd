using Domain.Primitives;

namespace Domain.Entities.Message.Event
{
    public record MessageCreatedDomainEvent(Message e) : DomainEvent(e)
    {
    }
}
