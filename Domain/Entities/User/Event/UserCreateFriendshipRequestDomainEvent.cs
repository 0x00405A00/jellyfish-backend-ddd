using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities.User.Event
{
    public record UserCreateFriendshipRequestDomainEvent : DomainEvent
    {
        private User user;
        private FriendshipRequest request;

        public UserCreateFriendshipRequestDomainEvent(User user, FriendshipRequest request) : base(user) 
        {
            this.user = user;
            this.request = request;
        }
    }
}