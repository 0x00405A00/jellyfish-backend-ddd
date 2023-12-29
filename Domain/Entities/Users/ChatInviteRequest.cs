using Domain.Entities.Chats;
using Domain.Entities.Users;
using Domain.Primitives;
using Shared.ValueObjects.Ids;

namespace Shared.Entities.Users
{
    public sealed partial class ChatInviteRequest : Entity<ChatInviteRequestId>
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
