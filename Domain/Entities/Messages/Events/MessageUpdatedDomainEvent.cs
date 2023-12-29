using Domain.Primitives;

namespace Domain.Entities.Messages.Events
{
    public record MessageUpdatedDomainEvent(Users.User updatedByUser, Message e) : DomainEvent(e)
    {
    }
}
