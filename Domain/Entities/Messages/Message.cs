using Domain.Entities.Chats;
using Domain.Entities.Messages.Events;
using Domain.Entities.Messages.Exceptions;
using Domain.Entities.Users;
using Domain.Exceptions;
using Domain.Extension;
using Domain.Primitives;
using Domain.Primitives.Ids;
using Domain.ValueObjects;

namespace Domain.Entities.Messages
{
    public interface IMessage
    {
        Chat Chat { get; }
        ChatId ChatForeignKey { get; }
        MediaContent? MediaContent { get; }
        Location? Location { get; }
        IReadOnlyCollection<MessageOutbox>? MessagesInOutbox { get; }
        string Text { get; }
        User User { get; }
        UserId UserForeignKey { get; }
    }
    public sealed partial class Message : AuditableEntity<MessageId>, IMessage
    {
        public ChatId ChatForeignKey { get; private set; }
        public UserId UserForeignKey { get; private set; }

        public string Text { get; private set; }

        public MediaContent? MediaContent { get; private set; }

        public Location? Location { get; private set; }

        private Message() : base()
        {
            
        }
        private Message(
            MessageId id,
            ChatId chatId,
            UserId userId,
            string text,
            MediaContent? mediaContent,
            CustomDateTime createdDateTime,
            UserId createdBy,
            CustomDateTime? modifiedDateTime,
            UserId? modifiedBy,
            CustomDateTime? deletedDateTime,
            UserId? deletedBy,
            Location? location = null) : base(id)
        {
            ChatForeignKey = chatId;
            UserForeignKey = userId;
            Text = text;
            MediaContent = mediaContent;

            CreatedTime = createdDateTime;
            CreatedByUserForeignKey = createdBy;
            LastModifiedTime = modifiedDateTime;
            LastModifiedByUserForeignKey = modifiedBy;
            DeletedTime = deletedDateTime;
            DeletedByUserForeignKey = deletedBy;

            Location = location;
        }
        /// <summary>
        /// Factory Method for creating a Message
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="chat">Normally give by Parent Object (Aggregate Root Chat) but is needed for ORM Mapping</param>
        /// <param name="owner"></param>
        /// <param name="text"></param>
        /// <param name="createdTime"></param>
        /// <param name="lastModifiedTime"></param>
        /// <param name="deletedTime"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public static Message Create(
            MessageId id,
            ChatId chatId,
            UserId userId,
            string text,
            MediaContent? mediaContent,
            CustomDateTime createdDateTime,
            UserId createdBy,
            CustomDateTime? modifiedDateTime,
            UserId? modifiedBy,
            CustomDateTime? deletedDateTime,
            UserId? deletedBy,
            Location? location = null)
        {
            if (!id.IsValid())
            {
                throw new NotValidEntityIdentification();
            }
            if (string.IsNullOrWhiteSpace(text) && (mediaContent == null||location == null))
            {
                throw new NotValidMessageException();
            }

            Message msg = new Message(
                id,
                chatId,
                userId,
                text,
                mediaContent,
                createdDateTime,
                createdBy,
                modifiedDateTime,
                modifiedBy,
                deletedDateTime,
                deletedBy,
                location);

            msg.Raise(new MessageCreatedDomainEvent(msg.Id));
            return msg;
        }
        public void UpdateText(UserId modifiedBy, string text)
        {
            if (modifiedBy == null || string.IsNullOrWhiteSpace(text))
            {
                throw new NotValidMessageException();
            }
            if (!IsMessageOwner(modifiedBy))
            {
                throw new NotMessageOwnerException();
            }
            Text = text;
            SetLastModified(modifiedBy);
            Raise(new MessageUpdatedDomainEvent(modifiedBy, this.Id));
        }
        public void UpdateMediaContent(UserId modifiedBy, MediaContent mediaContent)
        {
            if (modifiedBy == null || mediaContent == null)
            {
                throw new NotValidMessageException();
            }
            if (!IsMessageOwner(modifiedBy))
            {
                throw new NotMessageOwnerException();
            }
            MediaContent = mediaContent;
            SetLastModified(modifiedBy);
            Raise(new MessageUpdatedDomainEvent(modifiedBy, this.Id));
        }
        public void Delete(UserId deletedBy)
        {
            if (!IsMessageOwner(deletedBy))
            {
                throw new NotMessageOwnerException();
            }
            SetDeleted(deletedBy);
            Raise(new MessageRemovedDomainEvent(deletedBy, this.Id));
        }
        public bool IsMessageOwner(UserId user)
        {
            return user == this.UserForeignKey;
        }

    }
    public sealed partial class Message
    {
        public Chat Chat { get; }
        public User User { get; }
        public IReadOnlyCollection<MessageOutbox>? MessagesInOutbox { get; }
    }
}
