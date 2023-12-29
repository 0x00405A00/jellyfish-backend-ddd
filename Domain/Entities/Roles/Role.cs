using Domain.Entities.Roles.Events;
using Domain.Primitives;
using Shared.ValueObjects.Ids;

namespace Domain.Entities.Roles
{
    public sealed class Role : AuditableEntity<RoleId>
    {
        public string Name { get; set; }
        public string Description { get; set; }

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
            Users.User createdByUser,
            Users.User? modifiedByUser,
            Users.User? deletedByUser) : base(id)
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
            Users.User createdByUser,
            Users.User? modifiedByUser,
            Users.User? deletedByUser)
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


    }
}
