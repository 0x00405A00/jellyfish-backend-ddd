using Domain.Primitives;

namespace Domain.Entities.User
{
    public class UserId : Identification
    {
        public UserId()
        {
            
        }
        public UserId(Guid guid) : base(guid)
        {
        }
    }
}
