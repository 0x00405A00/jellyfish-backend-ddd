using Domain.Primitives;

namespace Domain.Entities.User
{
    public sealed class UserFriendId : Identification
    {
        public UserFriendId(Guid id ) : base( id )
        {
            
        }
    }
}
