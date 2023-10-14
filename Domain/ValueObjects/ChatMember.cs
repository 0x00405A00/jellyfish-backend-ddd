using Domain.Entities.Chats;
using Domain.Entities.User;
using Domain.Primitives;

namespace Domain.ValueObjects
{
    public sealed class ChatMember : IAuditibleCreateEntity, IAuditibleModifiedEntity, IAuditibleDeleteEntity
    {
        public User User { get; private set; }
        public bool IsAdmin { get; private set; }

        public DateTime? CreatedTime { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public User? CreatedByUser { get; set; }
        public User? LastModifiedByUser { get; set; }
        public User? DeletedByUser { get; set; }

        private ChatMember()
        {

        }
        private ChatMember(
            User user,
            bool isAdmin,   
            DateTime? createdTime,
            DateTime? lastModifiedTime,
            DateTime? deletedTime) 
        {
            IsAdmin = isAdmin;
            User = user;
            CreatedTime = createdTime;
            LastModifiedTime = lastModifiedTime;
            DeletedTime = deletedTime;
        }

        public static ChatMember Create(
            User user,
            bool isAdmin,
            DateTime createdTime,
            DateTime? lastModifiedTime,
            DateTime? deletedTime)
        {
            ChatMember userRole = new ChatMember(
                user,
                isAdmin,
                createdTime,
                lastModifiedTime,
                deletedTime);

            return userRole;
        }
        public void SetAdmin(bool admin)
        {
            IsAdmin = admin;
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
