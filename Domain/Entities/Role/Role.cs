using Domain.Entities.Role.Event;
using Domain.Primitives;

namespace Domain.Entities.Role
{
    public sealed class Role : Entity<RoleId>, IAuditibleCreateEntity, IAuditibleModifiedEntity, IAuditibleDeleteEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime? CreatedTime { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public User.User? CreatedByUser { get; set; }
        public User.User? LastModifiedByUser { get; set; }
        public User.User? DeletedByUser { get; set; }

        private Role()
        {
            
        }
        private Role(
            RoleId id,
            string name,
            string? description,
            DateTime? createdTime,
            DateTime? lastModifiedTime,
            DateTime? deletedTime,
            User.User createdByUser,
            User.User? modifiedByUser,
            User.User? deletedByUser) : base(id)
        {
            Name = name;
            Description = description;

            LastModifiedByUser = modifiedByUser;
            LastModifiedTime = lastModifiedTime;
            DeletedByUser = deletedByUser;
            DeletedTime = deletedTime;
            CreatedByUser = createdByUser;
            CreatedTime = createdTime;
        }

        public static Role Create(
            RoleId roleId,
            string name,
            string? description,
            DateTime createdTime,
            DateTime? lastModifiedTime,
            DateTime? deletedTime,    
            User.User createdByUser,
            User.User? modifiedByUser,
            User.User? deletedByUser)
        {
            Role role = new Role(
                roleId,
                name,
                description,
                createdTime,
                lastModifiedTime,
                deletedTime,
                createdByUser,
                modifiedByUser,
                deletedByUser);

            role.Raise(new RoleCreatedDomainEvent(role));
            return role;
        }

        public void SetLastModified(User.User modifiedBy)
        {
            this.LastModifiedTime = DateTime.Now;
            this.LastModifiedByUser = modifiedBy;
        }
        public void SetCreated(User.User createdBy)
        {
            this.CreatedTime = DateTime.Now;
            this.CreatedByUser = createdBy;
        }
        public void SetDeleted(User.User deletedBy)
        {
            this.DeletedTime = DateTime.Now;
            this.DeletedByUser = deletedBy;
        }

    }
}
