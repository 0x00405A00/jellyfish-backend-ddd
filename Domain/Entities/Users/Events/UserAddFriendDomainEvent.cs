using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Users.Events
{
    public record UserAddFriendDomainEvent(UserId UserId, UserId NewFriendId) : DomainEvent(UserId)
    {
    }
}
