﻿using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Users
{
    public interface IFriendshipRequest
    {
        public User? RequesterUser { get; set; }
        public UserId RequestUserForeignKey { get; }
        public User? TargetUser { get; set; }
        public UserId TargetUserForeignKey { get; }
        public string? TargetUserRequestMessage { get; }
    }
    /// <summary>
    /// Primary Key for a FriendshipRequest are RequestUser.Uuid and TargetUser.Uuid
    /// </summary>
    public sealed partial class FriendshipRequest : Entity<FriendshipRequestId>, IFriendshipRequest
    {
        public UserId RequestUserForeignKey { get; private set; }
        public UserId TargetUserForeignKey { get; private set; }

        public string? TargetUserRequestMessage { get; private set; }

        private FriendshipRequest() : base()
        {

        }
        public FriendshipRequest(
            FriendshipRequestId id,
            string targetUserRequestMessage,
            UserId requestUserId,
            UserId targetUserId,
            CustomDateTime createdDateTime,
            CustomDateTime? modifiedDateTime,
            CustomDateTime? deletedDateTime) : base(id)
        {
            RequestUserForeignKey = requestUserId;
            TargetUserForeignKey = targetUserId;
            TargetUserRequestMessage = targetUserRequestMessage;

            CreatedTime = createdDateTime;
            LastModifiedTime = modifiedDateTime;
            DeletedTime = deletedDateTime;
        }

        public static FriendshipRequest Create(
            FriendshipRequestId id,
            string targetUserRequestMessage,
            UserId requestUserId,
            UserId targetUserId,
            CustomDateTime createdDateTime,
            CustomDateTime? modifiedDateTime,
            CustomDateTime? deletedDateTime)
        {
            return new FriendshipRequest(
                id,
                targetUserRequestMessage,
                requestUserId,
                targetUserId,
                createdDateTime,
                modifiedDateTime,
                deletedDateTime);
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
            return RequestUserForeignKey == user.Id;
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
            return TargetUserForeignKey == user.Id;
        }
    }

    public sealed partial class FriendshipRequest
    {
        public User RequesterUser { get; set; }
        public User TargetUser { get; set; }
    }
}
