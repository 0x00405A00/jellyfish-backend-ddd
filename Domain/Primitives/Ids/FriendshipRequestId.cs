using Domain.Primitives;

namespace Domain.Primitives.Ids
{
    public record FriendshipRequestId : Identification
    {
        public FriendshipRequestId(Guid Uuid) : base(Uuid)
        {
        }
    }
}
