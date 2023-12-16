using Domain.Primitives;

namespace Domain.Entities.User
{
    public sealed class UserId : Identification
    {
        public UserId(Guid guid) : base(guid)
        {

        }
    }
}
