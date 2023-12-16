using Domain.Primitives;

namespace Domain.Entities.User
{
    public sealed class FriendshipRequestId : Identification
    {
        public FriendshipRequestId(Guid id) : base(id)
        {

        }
    }
}
