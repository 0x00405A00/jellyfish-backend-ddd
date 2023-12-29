using Domain.Primitives;

namespace Domain.Entities.Messages.Events
{
    public record MessageRemovedDomainEvent(Users.User deletedByUser, Message e) : DomainEvent(e)
    {
    }
}
