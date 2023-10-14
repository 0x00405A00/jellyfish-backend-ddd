using Domain.Primitives;

namespace Domain.Entities.User.Event
{
    public record UserRemoveFriendDomainEvent(User user, User newFriend) : DomainEvent(user)
    {
    }
}
