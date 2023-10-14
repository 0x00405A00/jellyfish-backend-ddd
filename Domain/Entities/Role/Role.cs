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
            DateTime? deletedTime) : base(id)
        {
            Name = name;
            Description = description;
            CreatedTime = createdTime;
            LastModifiedTime = lastModifiedTime;
            DeletedTime = deletedTime;
        }

        public static Role Create(
            RoleId roleId,
            string name,
            string? description,
            DateTime createdTime,
            DateTime? lastModifiedTime,
            DateTime? deletedTime)
        {
            Role role = new Role(
                roleId,
                name,
                description,
                createdTime,
                lastModifiedTime,
                deletedTime);

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
