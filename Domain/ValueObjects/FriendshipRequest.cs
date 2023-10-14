using Domain.Entities.User;
using Domain.Primitives;

namespace Domain.ValueObjects
{
    public class FriendshipRequest : IAuditibleCreateEntity
    {
        public User RequestUser { get; private set; }
        public User TargetUser { get; private set; }
        public string? TargetUserRequestMessage { get; private set; }
        public DateTime? CreatedTime { get; private set; }
        public User CreatedByUser { get; private set; }

        public FriendshipRequest(
            string targetUserRequestMessage,
            User requestUser,
            User targetUser)
        {

            TargetUserRequestMessage = targetUserRequestMessage;
            RequestUser = requestUser;
            TargetUser = targetUser;
        }

        public static FriendshipRequest Create(
            string? targetUserRequestMessage,
            User requestUser,
            User targetUser)
        {

            return new FriendshipRequest(targetUserRequestMessage,
                                         requestUser,
                                         targetUser);
        }

        public void SetCreated(User createdBy)
        {
            CreatedByUser = createdBy;
        }
    }
}
