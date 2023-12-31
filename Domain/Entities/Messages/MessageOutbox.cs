using Domain.Entities.Users;
using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Messages
{
    public interface IMessageOutbox
    {
        Message Message { get; }
        MessageId MessageForeignKey { get; }
        User User { get; }
        UserId UserForeignKey { get; }
    }

    public sealed partial class MessageOutbox : Entity<MessageOutboxId>, IMessageOutbox
    {
        public MessageId MessageForeignKey { get; private set; }
        public UserId UserForeignKey { get; private set; }

        private MessageOutbox() : base()
        {

        }
        private MessageOutbox(
            MessageOutboxId id,
            MessageId messageId,
            UserId userId,
            CustomDateTime createdDateTime,
            CustomDateTime? modifiedDateTime,
            CustomDateTime? deletedDateTime) : base(id)
        {
            MessageForeignKey = messageId;
            UserForeignKey = userId;
            CreatedTime = createdDateTime;
            LastModifiedTime = modifiedDateTime;
            DeletedTime = deletedDateTime;
        }

        public static MessageOutbox Create(
            MessageOutboxId id,
            MessageId messageId,
            UserId userId,
            CustomDateTime createdDateTime,
            CustomDateTime? modifiedDateTime,
            CustomDateTime? deletedDateTime)
        {
            return new MessageOutbox(
                id,
                messageId,
                userId,
                createdDateTime,
                modifiedDateTime,
                deletedDateTime);
        }
    }
    public sealed partial class MessageOutbox
    {
        public Message Message { get; }
        public User User { get; }
    }
}
