using Domain.Entities.Chats.Event;
using Domain.Entities.Chats.Exception;
using Domain.Entities.Message.Exception;
using Domain.Primitives;
using Domain.ValueObjects;
using System.Collections.Immutable;

namespace Domain.Entities.Chats
{
    /// <summary>
    /// Aggregate Root for BoundedContext Chat
    /// Included Entities: Chat (Aggregate), Message, User
    /// </summary>
    public sealed class Chat : Entity<ChatId>, IAuditibleCreateEntity, IAuditibleModifiedEntity, IAuditibleDeleteEntity
    {
        private ICollection<ChatMember> _members = new List<ChatMember>();
        private ICollection<Message.Message> _messages = new List<Message.Message>();

        public Picture Picture { get; private set; }
        public string ChatName { get; private set; }
        public string ChatDescription { get; private set; }
        public IReadOnlyCollection<ChatMember> Members { get => _members.ToImmutableList(); }
        public IReadOnlyCollection<ChatMember> Admins { get => _members.Where(x=>x.IsAdmin).ToImmutableList(); }
        public IReadOnlyCollection<Message.Message> Messages { get => _messages.ToImmutableList(); }

        public DateTime? CreatedTime { get; private set; }
        public DateTime? LastModifiedTime { get; private set; }
        public DateTime? DeletedTime { get; private set; }
        public User.User CreatedByUser { get; private set; }
        public User.User? LastModifiedByUser { get; private set; }
        public User.User? DeletedByUser { get; private set; }
        private Chat()
        {
            
        }
        private Chat(
            ChatId id,
            User.User createdByUser,
            string chatName,
            string? chatDescription,
            Picture? picture,
            ICollection<ChatMember> members,
            ICollection<Message.Message>? messages,
            DateTime createdTime,
            DateTime? lastModifiedTime,
            DateTime? deletedTime) : base(id)
        {
            SetCreated(createdByUser);
            ChatName = chatName;
            ChatDescription = chatDescription;
            Picture = picture;
            _members = members;
            _messages = messages??new List<Message.Message>();   
            LastModifiedTime = lastModifiedTime;
            DeletedTime = deletedTime;
        }
        /// <summary>
        /// Factory Method for Creating a Chat
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="user"></param>
        /// <param name="chatName"></param>
        /// <param name="chatPicture"></param>
        /// <param name="members"></param>
        /// <param name="messages"></param>
        /// <param name="createdTime"></param>
        /// <param name="lastModifiedTime"></param>
        /// <param name="deletedTime"></param>
        /// <returns></returns>
        public static Chat Create(
            ChatId chatId,
            User.User createdByUser,
            string chatName,
            string? chatDescription,
            Picture? picture,
            ICollection<ChatMember> members,
            ICollection<Message.Message> messages,
            DateTime createdTime,
            DateTime? lastModifiedTime,
            DateTime? deletedTime)
        {
            if(members == null|| members.Count<2)
            {
                throw new ChatCreateException("a new chat need minimum 2 members");
            }
            if(members!=null&& members.Any(x=>x==null))
            {
                throw new InvalidDataException($"item of list {nameof(members)} contains one or more items with null value");
            }
            if (members.Where(x => x.IsAdmin).Count() == 0)
            {
                throw new InvalidDataException($"the chat needs at least one admin");
            }
            Chat chat = new Chat(
                chatId,
                createdByUser,
                chatName,
                chatDescription,
                picture,
                members,
                messages,
                createdTime,
                lastModifiedTime,
                deletedTime);

            chat.Raise(new ChatCreatedDomainEvent(chat));
            return chat;
        }
        public void AddMember(User.User commandExecUser, User.User member)
        {
            if (commandExecUser == null)
            {
                throw new ArgumentNullException(nameof(commandExecUser));
            }
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }
            var t = IsChatAdmin(commandExecUser);
            if (!t)
            {
                throw new UserIsNoAdminInChatException("executive is no chat admin");
            }
            if (IsChatMember(member))
            {
                throw new UserAlreadyMemberInChatException();
            }
            var chatMember = ChatMember.Create(member, false, DateTime.Now, null, null);
            this._members.Add(chatMember);
            Raise(new ChatUserAddToChatDomainEvent(this, commandExecUser, member));
        }
        public void RemoveMember(User.User commandExecUser, User.User member)
        {
            if (commandExecUser == null)
            {
                throw new ArgumentNullException(nameof(commandExecUser));
            }
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            if (!IsChatAdmin(commandExecUser))
            {
                throw new UserIsNoAdminInChatException("executive is no chat admin");
            }
            if (!IsChatMember(member))
            {
                throw new UserIsNoMemberInChatException("member is not in chat");
            }
            var chatMember = this.GetMember(member);
            this._members.Remove(chatMember);
            Raise(new ChatUserRemoveFromChatDomainEvent(this, commandExecUser, member));
        }
        public void AssignAdmin(User.User commandExecUser, User.User member)
        {
            if (commandExecUser == null)
            {
                throw new ArgumentNullException(nameof(commandExecUser));
            }
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }
            if(!IsChatMember(member))
            {
                throw new UserIsNoMemberInChatException("target user is no chat member");
            }
            if (!IsChatAdmin(commandExecUser))
            {
                throw new UserIsNoAdminInChatException("executive is no chat admin");
            }
            if (IsChatAdmin(member))
            {
                throw new UserAlreadyAdminInChatException();
            }
            var chatMember = this.GetMember(member);
            chatMember.SetAdmin(true);
            Raise(new ChatUserAssignAdminChatDomainEvent(this, commandExecUser, member));
        }
        public void RevokeAdmin(User.User commandExecUser, User.User member)
        {
            if (commandExecUser == null)
            {
                throw new ArgumentNullException(nameof(commandExecUser));
            }
            if(member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }
            if (!IsChatAdmin(commandExecUser))
            {
                throw new UserIsNoAdminInChatException("executive is no chat admin");
            }
            if (!IsChatAdmin(member))
            {
                throw new UserIsNoMemberInChatException("member is not an admin");
            }

            var chatMember = this.GetMember(member);
            chatMember.SetAdmin(false);
            Raise(new ChatUserRevokeAdminDomainEvent(this, commandExecUser, member));
        }
        public Message.Message AddMessage(User.User messageOwner, string text, MediaContent? mediaContent)
        {
            if(messageOwner == null) 
            { 
                throw new ArgumentNullException(); 
            }
            if(String.IsNullOrWhiteSpace(text) && mediaContent == null)
            {
                throw new NotValidMessageException();
            }
            var message = Message.Message.Create(new Message.MessageId(Guid.NewGuid()), this, messageOwner, text, mediaContent, DateTime.Now, null, null);
            this._messages.Add(message);
            Raise(new ChatAppendMessageDomainEvent(this, messageOwner, message));
            return message;
        }
        public void RemoveMessage(User.User deletedByUser, Message.MessageId messageId)
        {
            if (deletedByUser == null)
            {
                throw new ArgumentNullException(nameof(deletedByUser));
            }
            var message = _messages.FirstOrDefault(x => x.Uuid == messageId);
            if (message == null)
            {
                throw new MessageNotFoundException("message is not found");
            }
            this._messages.Remove(message);
            Raise(new ChatRemoveMessageDomainEvent(this, deletedByUser,message.Owner, message));
        }
        public void UpdateMessage(User.User modifiedByUser, Message.MessageId messageId,string text,MediaContent? mediaContent)
        {
            if(modifiedByUser == null)
            {
                throw new ArgumentNullException();
            }
            var message = _messages.FirstOrDefault(x => x.Uuid == messageId);
            if (message == null)
            {
                throw new MessageNotFoundException("message is not found");
            }
            if (!String.IsNullOrWhiteSpace(text))
            {
                message.UpdateText(modifiedByUser, text);
            }
            if (mediaContent!=null)
            {
                message.UpdateMediaContent(modifiedByUser, mediaContent);
            }
            Raise(new ChatUpdateMessageDomainEvent(this, modifiedByUser, message.Owner, message));
        }
        public void UpdatePicture(User.User modifiedBy, Picture picture)
        {
            if (modifiedBy == null)
            {
                throw new ArgumentNullException();
            }
            if (!IsChatAdmin(modifiedBy))
            {
                throw new UserIsNoAdminInChatException("executive is no chat admin");
            }
            this.Picture = picture;
            SetLastModified(modifiedBy);
            Raise(new ChatUpdatedDomainEvent(this));
        }
        public void UpdateName(User.User modifiedBy, string name)
        {
            if(modifiedBy == null)
            {
                throw new ArgumentNullException();
            }
            if (!IsChatAdmin(modifiedBy))
            {
                throw new UserIsNoAdminInChatException("executive is no chat admin");
            }
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new NotValidChatDescriptionException("chat is null, empty or whitespace");
            }
            this.ChatName = name;
            SetLastModified(modifiedBy);
            Raise(new ChatUpdatedDomainEvent(this));
        }
        public void UpdateDescription(User.User modifiedBy, string text)
        {
            if (modifiedBy == null)
            {
                throw new ArgumentNullException();
            }
            if (!IsChatAdmin(modifiedBy))
            {
                throw new UserIsNoAdminInChatException("executive is no chat admin");
            }
            if (String.IsNullOrWhiteSpace(text))
            {
                throw new NotValidChatDescriptionException("description is null, empty or whitespace");
            }
            this.ChatDescription = text;
            SetLastModified(modifiedBy);
            Raise(new ChatUpdatedDomainEvent(this));
        }
        public void Remove(User.User deletedBy)
        {
            if (deletedBy == null)
            {
                throw new ArgumentNullException($"{nameof(deletedBy)} argument is null");
            }
            if(IsDeleted())
            {
                throw new ChatAlreadyDeletedException("chat is already deleted");
            }
            if (!IsChatAdmin(deletedBy))
            {
                throw new UserIsNoAdminInChatException("executive is no chat admin");
            }
            SetDeleted(deletedBy);
            Raise(new ChatDeletedDomainEvent(this));
        }
        public ChatMember GetMember(User.User user)
        {
            if(user == null)
            {
                return null;
            }
            var chatMember = _members.Where(x => x.User.Uuid == user.Uuid).First();
            return chatMember;
        }
        public bool IsChatMember(User.User user)
        {
            var result = user != null && _members.Any() && _members.Where(x => x.User.Uuid == user.Uuid).Count() != 0;
            return result;
        }
        public bool IsChatAdmin(User.User user)
        {
            var result = user != null && _members.Any() && _members.Where(x => x.User.Uuid == user.Uuid && x.IsAdmin).Count() != 0;
            return result;
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
        public bool IsDeleted() => this.DeletedTime != null;

    }
}
