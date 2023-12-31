using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Users.Events
{
    public record UserBlockUserDomainEvent(UserId UserId, UserId BlockedUserId) : DomainEvent(UserId)
    {
    }
}
