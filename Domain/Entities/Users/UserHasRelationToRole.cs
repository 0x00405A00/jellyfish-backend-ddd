using Domain.Const;
using Domain.Entities.Roles;
using Domain.Extension;
using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Users
{
    public sealed partial class UserHasRelationToRole : AuditableEntity<UserHasRelationToRoleId>
    {
        public UserId UserForeignKey { get; private set; }
        public RoleId RoleForeignKey { get; private set; }

        private UserHasRelationToRole() : base()
        {

        }

        private UserHasRelationToRole(
            UserHasRelationToRoleId id,
            UserId userId,
            RoleId roleId,
            CustomDateTime createdDateTime,
            UserId createdBy,
            CustomDateTime? modifiedDateTime,
            UserId? modifiedBy,
            CustomDateTime? deletedDateTime,
            UserId? deletedBy):base(id)
        {
            UserForeignKey = userId;
            RoleForeignKey = roleId;
            CreatedTime = createdDateTime;
            CreatedByUserForeignKey = createdBy;
            LastModifiedTime = modifiedDateTime;
            LastModifiedByUserForeignKey = modifiedBy;
            DeletedTime = deletedDateTime;
            DeletedByUserForeignKey = deletedBy;
        }

        public static UserHasRelationToRole Create(
            UserHasRelationToRoleId id,
            UserId userId,
            RoleId roleId,
            CustomDateTime createdDateTime,
            UserId createdBy,
            CustomDateTime? modifiedDateTime,
            UserId? modifiedBy,
            CustomDateTime? deletedDateTime,
            UserId? deletedBy)
        {
            UserHasRelationToRole userRole = new UserHasRelationToRole(
                id,
                userId,
                roleId,
                createdDateTime,
                createdBy,
                modifiedDateTime,
                modifiedBy,
                deletedDateTime,
                deletedBy);

            return userRole;
        }
        public static UserHasRelationToRole NewRoot(UserId userId,UserId assignerId=null)
        {
            return New(
                userId,
                RoleConst.RootRoleUuid.ToIdentification<RoleId>(),
                assignerId);
        }
        public static UserHasRelationToRole NewAdmin(UserId userId, UserId assignerId = null)
        {
            return New(
                userId,
                RoleConst.AdminRoleUuid.ToIdentification<RoleId>(),
                assignerId);
        }
        public static UserHasRelationToRole NewUser(UserId userId, UserId assignerId = null)
        {
            return New(
                userId,
                RoleConst.UserRoleUuid.ToIdentification<RoleId>(),
                assignerId);
        }
        private static UserHasRelationToRole New(UserId userId,RoleId roleId, UserId assignerId = null)
        {
            return UserHasRelationToRole.Create(
                UserHasRelationToRole.NewId(),
                userId,
                roleId,
                DateTime.Now.ToTypedDateTime(),
                assignerId ?? UserConst.RootUserId.ToIdentification<UserId>(),
                null,
                null,
                null,
                null);
        }
    }
    public sealed partial class UserHasRelationToRole
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
