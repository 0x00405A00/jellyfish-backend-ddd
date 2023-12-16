using Domain.Entities.User;
using Domain.Primitives;

namespace Domain.ValueObjects
{
    /// <summary>
    /// Primary Key for a FriendshipRequest are RequestUser.Uuid and TargetUser.Uuid
    /// </summary>
    public sealed class FriendshipRequest : Entity<FriendshipRequestId>,IAuditibleCreateEntity
    {
        public User RequestUser { get; private set; }
        public User TargetUser { get; private set; }
        public string? TargetUserRequestMessage { get; private set; }
        public DateTime? CreatedTime { get; private set; }
        public User CreatedByUser { get; private set; }

        public FriendshipRequest(
            FriendshipRequestId requestId,
            string targetUserRequestMessage,
            User requestUser,
            User targetUser)
        {
            Id = requestId;
            TargetUserRequestMessage = targetUserRequestMessage;
            RequestUser = requestUser;
            TargetUser = targetUser;
        }

        public static FriendshipRequest Create(
            FriendshipRequestId requestId,
            string? targetUserRequestMessage,
            User requestUser,
            User targetUser)
        {

            return new FriendshipRequest(requestId,
                                         targetUserRequestMessage,
                                         requestUser,
                                         targetUser);
        }

        /// <summary>
        /// Check if the given <see cref="user"/> is the initiator of the request
        /// </summary>
        /// <param name="user"></param>
        /// <returns>true if user is creator/initiator of the friendship request otherwise false</returns>
        /// <exception cref="ArgumentException"></exception>
        public bool AmIRequester(User user)
        {
            if (user is null)
            {
                throw new ArgumentException(nameof(user));
            }
            return this.RequestUser == user;
        }
        /// <summary>
        /// Check if the given <see cref="user"/> is the receiver of the request
        /// </summary>
        /// <param name="user"></param>
        /// <returns>true if user is receiver of the friendship request otherwise false</returns>
        /// <exception cref="ArgumentException"></exception>
        public bool AmIReceiver(User user)
        {
            if (user is null)
            {
                throw new ArgumentException(nameof(user));
            }
            return this.TargetUser == user;
        }

        public void SetCreated(User createdBy)
        {
            CreatedByUser = createdBy;
        }
    }
}
