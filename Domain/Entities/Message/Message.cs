using Domain.Entities.Chats;
using Domain.Entities.Message.Event;
using Domain.Entities.Message.Exception;
using Domain.Exceptions;
using Domain.Extension;
using Domain.Primitives;
using Domain.ValueObjects;

namespace Domain.Entities.Message
{
    public sealed class Message : Entity<MessageId>,IAuditibleCreateEntity, IAuditibleModifiedEntity, IAuditibleDeleteEntity
    {
        
        public User.User Owner { get; private set; }
        public string Text { get; private set; }
        public MediaContent? MediaContent { get; private set; }

        public DateTime? CreatedTime { get; private set; }
        public ChatId Chat { get; }
        public DateTime? LastModifiedTime { get; private set; }
        public DateTime? DeletedTime { get; private set; }
        public User.User? CreatedByUser { get; private set; }
        public User.User? LastModifiedByUser { get; private set; }
        public User.User? DeletedByUser { get; private set; }
        private Message()
        {
            
        }
        private Message(
            MessageId id,
            ChatId chat,
            User.User owner,
            User.User? modifiedByUser,
            User.User? deletedByUser,
            string text,
            MediaContent? mediaContent,
            DateTime createdTime,
            DateTime? lastModifiedTime,
            DateTime? deletedTime) : base(id)
        {
            Chat = chat;
            Owner = owner;
            Text = text;
            MediaContent = mediaContent;

            LastModifiedByUser = modifiedByUser;
            LastModifiedTime = lastModifiedTime;
            DeletedByUser = deletedByUser;
            DeletedTime = deletedTime;
            CreatedByUser = owner;
            CreatedTime = createdTime;
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
        /// <returns></returns>
        public static Message Create(
            MessageId messageId,
            ChatId chat,
            User.User owner,
            User.User? modifiedByUser,
            User.User? deletedByUser,
            string text,
            MediaContent? mediaContent,
            DateTime createdTime,
            DateTime? lastModifiedTime,
            DateTime? deletedTime)
        {
            if(!messageId.IsValid())
            {
                throw new NotValidEntityIdentification();
            }
            if((String.IsNullOrWhiteSpace(text)&&mediaContent ==null) || owner == null)
            {
                throw new NotValidMessageException();   
            }

            Message msg = new Message(messageId,chat,owner,modifiedByUser,deletedByUser,text, mediaContent, createdTime,lastModifiedTime,deletedTime);

            msg.Raise(new MessageCreatedDomainEvent(msg));
            return msg;
        }
        public void UpdateText(User.User modifiedBy,string text)
        {
            if (modifiedBy == null||String.IsNullOrWhiteSpace(text))
            {
                throw new NotValidMessageException();
            }
            if (!IsMessageOwner(modifiedBy))
            {
                throw new NotMessageOwnerException();
            }
            this.Text = text;
            SetLastModified(modifiedBy);
            Raise(new MessageUpdatedDomainEvent(modifiedBy,this));
        }
        public void UpdateMediaContent(User.User modifiedBy,MediaContent mediaContent)
        {
            if (modifiedBy == null || mediaContent == null)
            {
                throw new NotValidMessageException();
            }
            if (!IsMessageOwner(modifiedBy))
            {
                throw new NotMessageOwnerException();
            }
            this.MediaContent = mediaContent;
            SetLastModified(modifiedBy);
            Raise(new MessageUpdatedDomainEvent(modifiedBy, this));
        }
        public void Delete(User.User deletedBy)
        {
            if (!IsMessageOwner(deletedBy))
            {
                throw new NotMessageOwnerException();
            }
            SetDeleted(deletedBy);
            Raise(new MessageRemovedDomainEvent(deletedBy,this));
        }
        public bool IsMessageOwner(User.User user)
        {
            return (user == this.Owner);
        }
        public void SetLastModified(User.User modifiedBy)
        {
            this.LastModifiedTime = DateTime.Now;
            this.LastModifiedByUser = modifiedBy;
        }
        public void SetCreated(User.User createdBy)
        {
            this.CreatedTime = DateTime.Now;
            this.CreatedByUser = createdBy;
        }
        public void SetDeleted(User.User deletedBy)
        {
            this.DeletedTime = DateTime.Now;
            this.DeletedByUser = deletedBy;
        }

    }
}
