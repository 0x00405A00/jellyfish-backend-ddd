using Domain.Primitives;

namespace Domain.Entities.Message.Event
{
    public record MessageRemovedDomainEvent(User.User deletedByUser,Message e) : DomainEvent(e)
    {
    }
}
