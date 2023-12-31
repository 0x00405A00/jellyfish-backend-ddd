using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Users.Events
{
    public record UserCreateFriendshipRequestDomainEvent : DomainEvent
    {
        private UserId user;
        private FriendshipRequestId request;

        public UserCreateFriendshipRequestDomainEvent(UserId UserId, FriendshipRequestId RequestId) : base(UserId)
        {
            this.user = UserId;
            this.request = RequestId;
        }
    }
}