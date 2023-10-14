using Domain.Primitives;

namespace Domain.Entities.Message.Event
{
    public record MessageUpdatedDomainEvent(User.User updatedByUser, Message e) : DomainEvent(e)
    {
    }
}
