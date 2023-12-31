using Domain.Entities.Users;
using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Roles
{
    public sealed partial class Role : AuditableEntity<RoleId>
    {
        public string Name { get; set; }

        private Role():base()
        {

        }

        private Role(
            RoleId id,
            string name,
            CustomDateTime createdDateTime,
            UserId createdBy,
            CustomDateTime? modifiedDateTime,
            UserId? modifiedBy,
            CustomDateTime? deletedDateTime,
            UserId? deletedBy) : base(id)
        {
            Name = name;
            CreatedTime = createdDateTime;
            CreatedByUserForeignKey = createdBy;
            LastModifiedTime = modifiedDateTime;
            LastModifiedByUserForeignKey = modifiedBy;
            DeletedTime = deletedDateTime;
            DeletedByUserForeignKey = deletedBy;
        }

        public static Role Create(
            RoleId id,
            string name,
            CustomDateTime createdDateTime,
            UserId createdBy,
            CustomDateTime? modifiedDateTime,
            UserId? modifiedBy,
            CustomDateTime? deletedDateTime,
            UserId? deletedBy)
        {
            return new Role(
                id,
                name,
                createdDateTime,
                createdBy,
                modifiedDateTime,
                modifiedBy,
                deletedDateTime,
                deletedBy);
        }
    }
    public sealed partial class Role
    {
        public ICollection<UserHasRelationToRole> UserHasRelationToRoles { get; }
    }
}
