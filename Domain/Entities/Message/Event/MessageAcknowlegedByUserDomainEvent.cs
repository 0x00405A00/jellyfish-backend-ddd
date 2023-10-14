using Domain.Primitives;

namespace Domain.Entities.Message.Event
{
    public record MessageAcknowlegedByUserDomainEvent(Message e, User.User user) : DomainEvent(e)
    {
    }
}
