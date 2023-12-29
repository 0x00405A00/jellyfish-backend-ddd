using Domain.Primitives;

namespace Shared.ValueObjects.Ids
{
    public record FriendshipRequestId : Identification
    {
        public FriendshipRequestId(Guid Uuid) : base(Uuid)
        {
        }
    }
}
