using Domain.Primitives;

namespace Domain.Primitives.Ids
{
    public record UserHasRelationToRoleId : Identification
    {
        public UserHasRelationToRoleId(Guid guid) : base(guid)
        {

        }
    }
}
