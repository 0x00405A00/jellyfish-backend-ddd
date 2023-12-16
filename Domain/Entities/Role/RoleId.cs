using Domain.Primitives;

namespace Domain.Entities.Role
{
    public sealed class RoleId : Identification
    {
        public RoleId(Guid guid) : base(guid)
        {
        }
    }
}
