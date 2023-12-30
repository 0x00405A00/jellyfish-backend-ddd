using Domain.Primitives;

namespace Domain.Primitives.Ids
{
    public record UserHasRelationToFriendId : Identification
    {
        public UserHasRelationToFriendId(Guid Uuid) : base(Uuid)
        {
        }
    }
}
