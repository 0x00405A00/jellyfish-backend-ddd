using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Users
{
    public sealed partial class UserHasRelationToFriend : AuditableEntity<UserHasRelationToFriendId>
    {
        public UserId UserForeignKey { get; private set; }
        public UserId UserFriendForeignKey { get; private set; }

        private UserHasRelationToFriend():base()
        {

        }

        private UserHasRelationToFriend(
            UserHasRelationToFriendId id,
            UserId userId,
            UserId friendId,
            CustomDateTime createdDateTime,
            UserId createdBy,
            CustomDateTime? modifiedDateTime,
            UserId? modifiedBy,
            CustomDateTime? deletedDateTime,
            UserId? deletedBy) :base(id)
        {
            UserForeignKey = userId;
            UserFriendForeignKey = friendId;
            CreatedTime = createdDateTime;
            CreatedByUserForeignKey = createdBy;
            LastModifiedTime = modifiedDateTime;
            LastModifiedByUserForeignKey = modifiedBy;
            DeletedTime = deletedDateTime;
            DeletedByUserForeignKey = deletedBy;
        }

        public static UserHasRelationToFriend Create(
            UserHasRelationToFriendId id,
            UserId userId,
            UserId friendId,
            CustomDateTime createdDateTime,
            UserId createdBy,
            CustomDateTime? modifiedDateTime,
            UserId? modifiedBy,
            CustomDateTime? deletedDateTime,
            UserId? deletedBy)
        {

            UserHasRelationToFriend userFriend = new UserHasRelationToFriend(
                id,
                userId,
                friendId,
                createdDateTime,
                createdBy,
                modifiedDateTime,
                modifiedBy,
                deletedDateTime,
                deletedBy);
            return userFriend;
        }
    }
    public sealed partial class UserHasRelationToFriend
    {
        public User User { get; set; }
        public User UserFriend { get; set; }
    }
}
