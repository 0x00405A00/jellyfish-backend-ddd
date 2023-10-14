using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities.User.Event
{
    public record UserAcceptFriendshipRequestDomainEvent : DomainEvent
    {
        private User user;
        private FriendshipRequest request;

        public UserAcceptFriendshipRequestDomainEvent(User user, FriendshipRequest request) : base(user) 
        {
            this.user = user;
            this.request = request;
        }
    }
}