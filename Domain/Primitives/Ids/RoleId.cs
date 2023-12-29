using Domain.Primitives;

namespace Shared.ValueObjects.Ids
{
    public record RoleId : Identification
    {
        public RoleId(Guid guid) : base(guid)
        {

        }
    }
}
