using Domain.Primitives;

namespace Domain.Entities.Users.Events
{
    public record UserBlockUserDomainEvent(User e, User blockedUser) : DomainEvent(e)
    {
    }
}
