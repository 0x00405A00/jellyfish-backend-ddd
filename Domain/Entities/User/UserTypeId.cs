using Domain.Primitives;

namespace Domain.Entities.User
{
    public sealed class UserTypeId : Identification
    {
        public UserTypeId(Guid guid) : base(guid)
        {
        }
    }

}
