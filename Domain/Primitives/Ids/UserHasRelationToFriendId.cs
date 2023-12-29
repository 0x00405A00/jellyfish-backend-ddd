using Domain.Primitives;

namespace Shared.ValueObjects.Ids
{
    public record UserHasRelationToFriendId : Identification
    {
        public UserHasRelationToFriendId(Guid Uuid) : base(Uuid)
        {
        }
    }
}
