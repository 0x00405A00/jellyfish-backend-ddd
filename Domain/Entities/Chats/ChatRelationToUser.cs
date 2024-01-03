using Domain.Const;
using Domain.Entities.Users;
using Domain.Extension;
using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Chats
{
    public interface IChatRelationToUser
    {
        Chat Chat { get; set; }
        ChatId ChatForeignKey { get; }
        bool? IsChatAdmin { get; }
        User User { get; set; }
        UserId UserForeignKey { get; }
    }

    public sealed partial class ChatRelationToUser : AuditableEntity<ChatRelationToUserId>, IChatRelationToUser
    {
        public UserId UserForeignKey { get; private set; }
        public ChatId ChatForeignKey { get; private set; }

        public bool? IsChatAdmin { get; private set; }

        private ChatRelationToUser() : base()
        {

        }
        private ChatRelationToUser(
            ChatRelationToUserId id,
            UserId userId,
            ChatId chatId,
            bool? isChatAdmin,
            CustomDateTime createdDateTime,
            UserId createdBy,
            CustomDateTime? modifiedDateTime,
            UserId? modifiedBy,
            CustomDateTime? deletedDateTime,
            UserId? deletedBy) : base(id)
        {
            UserForeignKey = userId;
            ChatForeignKey = chatId;
            IsChatAdmin = isChatAdmin;
            CreatedTime = createdDateTime;
            CreatedByUserForeignKey = createdBy;
            LastModifiedTime = modifiedDateTime;
            LastModifiedByUserForeignKey = modifiedBy;
            DeletedTime = deletedDateTime;
            DeletedByUserForeignKey = deletedBy;
        }

        public static ChatRelationToUser Create(
            ChatRelationToUserId id,
            UserId userId,
            ChatId chatId,
            bool? isChatAdmin,
            CustomDateTime createdDateTime,
            UserId createdBy,
            CustomDateTime? modifiedDateTime,
            UserId? modifiedBy,
            CustomDateTime? deletedDateTime,
            UserId? deletedBy)
        {
            return new ChatRelationToUser(
                id,
                userId,
                chatId,
                isChatAdmin,
                createdDateTime,
                createdBy,
                modifiedDateTime,
                modifiedBy,
                deletedDateTime,
                deletedBy);
        }

        public static ChatRelationToUser NewAdmin(ChatId chatId, UserId userId, UserId assignerId = null)
        {
            var id = ChatRelationToUser.NewId();
            bool isAdmin = true;
            CustomDateTime createdDateTime = DateTime.Now.ToTypedDateTime();
            return Create(
                id,
                userId,
                chatId,
                isAdmin,
                createdDateTime,
                assignerId,
                null,
                null,
                null,
                null);
        }
        public static ChatRelationToUser NewMember(ChatId chatId,UserId userId, UserId inviteMember = null)
        {
            var id = ChatRelationToUser.NewId();
            bool isAdmin = false;
            CustomDateTime createdDateTime = DateTime.Now.ToTypedDateTime();
            return Create(
                id,
                userId,
                chatId,
                isAdmin,
                createdDateTime,
                inviteMember,
                null,
                null,
                null,
                null);
        }

        public void SetAdmin(bool isAdmin)
        {
            IsChatAdmin = isAdmin;
        }
    }
    public sealed partial class ChatRelationToUser
    {
        public User User { get; set; }
        public Chat Chat { get; set; }
    }
}
