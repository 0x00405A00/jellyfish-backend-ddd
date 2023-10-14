using Domain.Primitives;

namespace Domain.Entities.User.Event
{
    public record UserAddFriendDomainEvent(User e, User newFriend) : DomainEvent(e)
    {
    }
}
