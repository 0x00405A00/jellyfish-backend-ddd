using Domain.Entities.Role;
using Domain.Entities.User;
using Domain.Primitives;

namespace Domain.ValueObjects
{
    public sealed class UserRole : IAuditibleCreateEntity, IAuditibleModifiedEntity, IAuditibleDeleteEntity
    {
        public User User { get; private set; }
        public Role Role { get; private set; }

        public DateTime? CreatedTime { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public User? CreatedByUser { get; set; }
        public User? LastModifiedByUser { get; set; }
        public User? DeletedByUser { get; set; }

        private UserRole()
        {

        }
        private UserRole(
            User user,
            Role role,
            DateTime? createdTime,
            DateTime? lastModifiedTime,
            DateTime? deletedTime) 
        {
            User = user;
            Role = role;
            CreatedTime = createdTime;
            LastModifiedTime = lastModifiedTime;
            DeletedTime = deletedTime;
        }

        public static UserRole Create(
            User user,
            Role role,
            DateTime createdTime,
            DateTime? lastModifiedTime,
            DateTime? deletedTime)
        {
            UserRole userRole = new UserRole(
                user,
                role,
                createdTime,
                lastModifiedTime,
                deletedTime);

            return userRole;
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
