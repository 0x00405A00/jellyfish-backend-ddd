using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Users.Events
{
    public record UserAcceptFriendshipRequestDomainEvent : DomainEvent
    {
        private UserId user;
        private FriendshipRequestId request;

        public UserAcceptFriendshipRequestDomainEvent(UserId UserId, FriendshipRequestId RequestId) : base(UserId)
        {
            this.user = UserId;
            this.request = RequestId;
        }
    }
}