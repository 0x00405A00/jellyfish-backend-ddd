﻿using Domain.Entities.Chats;
using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Users
{
    public interface IChatInviteRequest
    {
        Chat Chat { get; set; }
        ChatId ChatForeignKey { get; }
        User RequesterUser { get; set; }
        UserId RequesterUserForeignKey { get; }
        User TargetUser { get; set; }
        UserId TargetUserForeignKey { get; }
        string? TargetUserRequestMessage { get; }
    }
    public sealed partial class ChatInviteRequest : Entity<ChatInviteRequestId>, IChatInviteRequest
    {
        public ChatId ChatForeignKey { get; private set; }
        public UserId RequesterUserForeignKey { get; private set; }
        public UserId TargetUserForeignKey { get; private set; }

        public string? TargetUserRequestMessage { get; private set; }

        private ChatInviteRequest() : base()
        {

        }
        private ChatInviteRequest(
            ChatInviteRequestId id,
            ChatId chatId,
            UserId requesterUser,
            UserId targetUser,
            string targetUserRequestMessage,
            CustomDateTime createdDateTime,
            CustomDateTime? modifiedDateTime,
            CustomDateTime? deletedDateTime) : base(id)
        {
            ChatForeignKey = chatId;
            RequesterUserForeignKey = requesterUser;
            TargetUserForeignKey = targetUser;
            TargetUserRequestMessage = targetUserRequestMessage;
            CreatedTime = createdDateTime;
            LastModifiedTime = modifiedDateTime;
            DeletedTime = deletedDateTime;
        }

        public static ChatInviteRequest Create(
            ChatInviteRequestId id,
            ChatId chatId,
            UserId requesterUser,
            UserId targetUser,
            string targetUserRequestMessage,
            CustomDateTime createdDateTime,
            CustomDateTime? modifiedDateTime,
            CustomDateTime? deletedDateTime)
        {
            return new ChatInviteRequest(
                id,
                chatId,
                requesterUser,
                targetUser,
                targetUserRequestMessage,
                createdDateTime,
                modifiedDateTime,
                deletedDateTime);
        }
    }

    public sealed partial class ChatInviteRequest
    {
        public Chat Chat { get; set; }
        public User RequesterUser { get; set; }
        public User TargetUser { get; set; }
    }
}
