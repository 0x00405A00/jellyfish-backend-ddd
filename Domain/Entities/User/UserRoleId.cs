using Domain.Primitives;

namespace Domain.Entities.User
{
    public sealed class UserRoleId : Identification
    {
        public UserRoleId(Guid guid) : base(guid)
        {
        }
    }

}
