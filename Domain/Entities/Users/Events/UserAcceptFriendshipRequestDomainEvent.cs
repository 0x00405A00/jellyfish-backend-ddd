using Domain.Entities.Users;
using Domain.Primitives;

namespace Domain.Entities.Users.Events
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