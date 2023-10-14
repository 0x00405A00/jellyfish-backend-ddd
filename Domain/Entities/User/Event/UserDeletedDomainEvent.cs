using Domain.Primitives;

namespace Domain.Entities.User.Event
{
    public record UserDeletedDomainEvent(User e) : DomainEvent(e)
    {
    }
}
