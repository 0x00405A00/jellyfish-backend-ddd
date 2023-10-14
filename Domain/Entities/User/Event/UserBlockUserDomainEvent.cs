using Domain.Primitives;

namespace Domain.Entities.User.Event
{
    public record UserBlockUserDomainEvent(User e, User blockedUser) : DomainEvent(e)
    {
    }
}
