using Domain.Primitives;

namespace Shared.ValueObjects.Ids
{
    public record UserHasRelationToRoleId : Identification
    {
        public UserHasRelationToRoleId(Guid guid) : base(guid)
        {

        }
    }
}
