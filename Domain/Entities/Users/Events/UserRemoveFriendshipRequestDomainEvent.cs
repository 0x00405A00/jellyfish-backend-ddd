using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Users.Events
{
    public record UserRemoveFriendshipRequestDomainEvent : DomainEvent
    {
        private UserId user;
        private FriendshipRequestId request;

        public UserRemoveFriendshipRequestDomainEvent(UserId UserId, FriendshipRequestId RequestId) : base(UserId)
        {
            user = UserId;
            request = RequestId;
        }
    }
}