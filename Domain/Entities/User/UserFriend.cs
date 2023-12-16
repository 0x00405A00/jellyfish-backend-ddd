using Domain.Primitives;

namespace Domain.Entities.User
{
    public sealed class UserFriend : Entity<UserFriendId>, IAuditibleCreateEntity, IAuditibleModifiedEntity, IAuditibleDeleteEntity
    {
        public User User { get; private set; }
        public User Friend { get; private set; }

        public DateTime? CreatedTime { get; private set; }
        public DateTime? LastModifiedTime { get; private set; }
        public DateTime? DeletedTime { get; private set; }
        public User? CreatedByUser { get; private set; }
        public User? LastModifiedByUser { get; private set; }
        public User? DeletedByUser { get; private set; }

        private UserFriend()
        {

        }

        private UserFriend(UserFriendId userFriendId,
                           User user,
                           User friend,
                           DateTime createdTime,
                           DateTime? lastModifiedTime,
                           DateTime? deletedTime)
        {
            Id = userFriendId;    
            User = user;
            Friend = friend;
            CreatedTime = createdTime;
            LastModifiedTime = lastModifiedTime;
            DeletedTime = deletedTime;
        }

        public static UserFriend Create(
            UserFriendId userFriendId,
            User user,
            User friend,
            DateTime createdTime,
            DateTime? lastModifiedTime,
            DateTime? deletedTime,
            User createdBy)
        {

            UserFriend userFriend = new UserFriend(
                userFriendId,
                user,
                friend,
                createdTime,
                lastModifiedTime,
                deletedTime);
            userFriend.SetCreated(createdBy);
            return userFriend;
        }

        public void SetLastModified(User modifiedBy)
        {
            LastModifiedTime = DateTime.Now;
            LastModifiedByUser = modifiedBy;
        }

        public void SetCreated(User createdBy)
        {
            CreatedTime = DateTime.Now;
            CreatedByUser = createdBy;
        }

        public void SetDeleted(User deletedBy)
        {
            DeletedTime = DateTime.Now;
            DeletedByUser = deletedBy;
        }
    }
}
