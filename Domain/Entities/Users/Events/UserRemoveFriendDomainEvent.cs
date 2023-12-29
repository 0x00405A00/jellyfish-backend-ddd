using Domain.Primitives;

namespace Domain.Entities.Users.Events
{
    public record UserRemoveFriendDomainEvent(User user, User newFriend) : DomainEvent(user)
    {
    }
}
