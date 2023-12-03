using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities.User.Event
{
    public record UserRemoveFriendshipRequestDomainEvent : DomainEvent
    {
        private User user;
        private FriendshipRequest request;

        public UserRemoveFriendshipRequestDomainEvent(User User, FriendshipRequest Request) : base(User)
        {
            this.user = User;
            this.request = Request;
        }
    }
}