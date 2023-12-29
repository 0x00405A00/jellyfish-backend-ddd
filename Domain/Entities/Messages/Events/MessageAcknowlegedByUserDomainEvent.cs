using Domain.Primitives;

namespace Domain.Entities.Messages.Events
{
    public record MessageAcknowlegedByUserDomainEvent(Message e, Users.User user) : DomainEvent(e)
    {
    }
}
