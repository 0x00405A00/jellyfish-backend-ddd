using Domain.Primitives;

namespace Domain.Entities.User
{
    public class UserTypeId : Identification
    {
        public UserTypeId()
        {
            
        }
        public UserTypeId(Guid guid) : base(guid)
        {
        }
    }
}
