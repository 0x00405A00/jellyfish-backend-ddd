using Domain.Primitives;

namespace Domain.Entities.Role
{
    public class RoleId : Identification
    {
        public RoleId()
        {
            
        }
        public RoleId(Guid guid) : base(guid)
        {
        }
    }
}
