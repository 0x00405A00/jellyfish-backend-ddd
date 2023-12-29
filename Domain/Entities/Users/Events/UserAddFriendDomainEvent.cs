using Domain.Primitives;

namespace Domain.Entities.Users.Events
{
    public record UserAddFriendDomainEvent(User e, User newFriend) : DomainEvent(e)
    {
    }
}
