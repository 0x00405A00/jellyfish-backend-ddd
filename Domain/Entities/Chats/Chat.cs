﻿using Domain.Entities.Chats.Events;
using Domain.Entities.Chats.Exceptions;
using Domain.Entities.Messages;
using Domain.Entities.Messages.Exceptions;
using Domain.Entities.Users;
using Domain.Extension;
using Domain.Primitives;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using System.Collections.Immutable;

namespace Domain.Entities.Chats
{
    public interface IChat
    {
        IReadOnlyCollection<ChatRelationToUser> Admins { get; }
        IReadOnlyCollection<ChatInviteRequest>? ChatInvitesToUsers { get; }
        IReadOnlyCollection<ChatRelationToUser>? ChatRelationToUsers { get; }
        string Description { get; }
        IReadOnlyCollection<ChatRelationToUser> Members { get; }
        IReadOnlyCollection<Message> Messages { get; }
        string Name { get; }
        Picture? Picture { get; }
    }

    /// <summary>
    /// Aggregate Root for BoundedContext Chat
    /// Included Entities: Chat (Aggregate), Message, User
    /// </summary>
    public sealed partial class Chat : AuditableEntity<ChatId>, IChat
    {
        private List<ChatRelationToUser> _members = new();
        private List<ChatInviteRequest> _invites = new();
        private List<Message> _messages = new();

        public Picture? Picture { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }

        public IReadOnlyCollection<ChatRelationToUser> Members => _members.ToImmutableList();
        public IReadOnlyCollection<ChatRelationToUser> Admins => _members.Where(x => x.IsChatAdmin ?? false).ToList().ToImmutableList();

        private Chat() : base()
        {

        }
        private Chat(
            ChatId id,
            string name,
            string description,
            Picture? picture,
            List<ChatRelationToUser> members,
            CustomDateTime createdDateTime,
            UserId createdBy,
            CustomDateTime? modifiedDateTime,
            UserId? modifiedBy,
            CustomDateTime? deletedDateTime,
            UserId? deletedBy) : base(id)
        {
            Name = name;
            Description = description;
            Picture = picture;
            CreatedTime = createdDateTime;
            CreatedByUserForeignKey = createdBy;
            LastModifiedTime = modifiedDateTime;
            LastModifiedByUserForeignKey = modifiedBy;
            DeletedTime = deletedDateTime;
            DeletedByUserForeignKey = deletedBy;

            _members = members;
        }
        /// <summary>
        /// Factory Method for Creating a Chat
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="picture"></param>
        /// <param name="members"></param>
        /// <param name="createdDateTime"></param>
        /// <param name="createdBy"></param>
        /// <param name="modifiedDateTime"></param>
        /// <param name="modifiedBy"></param>
        /// <param name="deletedDateTime"></param>
        /// <param name="deletedBy"></param>
        /// <returns></returns>
        /// <exception cref="ChatCreateException"></exception>
        /// <exception cref="InvalidDataException"></exception>
        public static Chat Create(
            ChatId id,
            string name,
            string? description,
            Picture? picture,
            List<ChatRelationToUser> members,
            CustomDateTime createdDateTime,
            UserId createdBy,
            CustomDateTime? modifiedDateTime,
            UserId? modifiedBy,
            CustomDateTime? deletedDateTime,
            UserId? deletedBy)
        {
            if (members == null || members.Count < 2)
            {
                throw new ChatCreateException("a new chat need minimum 2 members");
            }
            if (members != null && members.Any(x => x == null))
            {
                throw new InvalidDataException($"item of list {nameof(members)} contains one or more items with null value");
            }
            if (members.Where(x => x.IsChatAdmin ?? false).Count() == 0)
            {
                throw new InvalidDataException($"the chat needs at least one admin");
            }
            Chat chat = new Chat(
                id,
                name,
                description,
                picture,
                members,
                createdDateTime,
                createdBy,
                modifiedDateTime,
                modifiedBy,
                deletedDateTime,
                deletedBy);

            chat.Raise(new ChatCreatedDomainEvent(chat.Id));
            return chat;
        }
        public void AddMember(UserId commandExecUser, UserId member)
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
                throw new UserAlreadyMemberInChatException("user is already chat-member");
            }
            var chatMember = ChatRelationToUser.Create(
                ChatRelationToUser.NewId(),
                member,
                this.Id,
                false,
                DateTime.UtcNow.ToTypedDateTime(),
                commandExecUser,
                null,
                null,
                null,
                null);
            this._members.Add(chatMember);
            Raise(new ChatUserAddToChatDomainEvent(this.Id, commandExecUser, member));
        }
        public void RemoveMember(UserId commandExecUser, UserId member)
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
            Raise(new ChatUserRemoveFromChatDomainEvent(this.Id, commandExecUser, member));
        }
        public void AssignAdmin(UserId commandExecUser, UserId member)
        {
            if (commandExecUser == null)
            {
                throw new ArgumentNullException(nameof(commandExecUser));
            }
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }
            if (!IsChatMember(member))
            {
                throw new UserIsNoMemberInChatException("target user is no chat member");
            }
            if (!IsChatAdmin(commandExecUser))
            {
                throw new UserIsNoAdminInChatException("executive is no chat admin");
            }
            if (IsChatAdmin(member))
            {
                throw new UserAlreadyAdminInChatException("user is already an admin");
            }
            var chatMember = this.GetMember(member);
            chatMember.SetAdmin(true);
            Raise(new ChatUserAssignAdminChatDomainEvent(this.Id, commandExecUser, member));
        }
        public void RevokeAdmin(UserId commandExecUser, UserId member)
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
            if (!IsChatAdmin(member))
            {
                throw new UserIsNoMemberInChatException("member is not an admin");
            }

            var chatMember = this.GetMember(member);
            chatMember.SetAdmin(false);
            Raise(new ChatUserRevokeAdminDomainEvent(this.Id, commandExecUser, member));
        }
        public Message AddMessage(UserId messageOwner, string text, MediaContent? mediaContent = null)
        {
            if (messageOwner is null)
            {
                throw new ArgumentNullException();
            }
            if (String.IsNullOrWhiteSpace(text) && mediaContent is null)
            {
                throw new NotValidMessageException();
            }
            var message = Message.Create(
                Message.NewId(),
                this.Id,
                messageOwner,
                text,
                mediaContent,
                DateTime.UtcNow.ToTypedDateTime(),
                messageOwner,
                null,
                null,
                null,
                null);
            this._messages.Add(message);
            Raise(new ChatAppendMessageDomainEvent(this.Id, message.UserForeignKey, message.Id));
            return message;
        }
        public Message AddMessage(Message message)
        {
            this._messages.Add(message);
            Raise(new ChatAppendMessageDomainEvent(this.Id, message.UserForeignKey, message.Id));
            return message;
        }
        public void RemoveMessage(UserId deletedByUser, MessageId messageId)
        {
            if (deletedByUser == null)
            {
                throw new ArgumentNullException(nameof(deletedByUser));
            }
            var message = _messages.FirstOrDefault(x => x.Id == messageId);
            if (message == null)
            {
                throw new MessageNotFoundException("message is not found");
            }
            this._messages.Remove(message);
            Raise(new ChatRemoveMessageDomainEvent(this.Id, deletedByUser, message.UserForeignKey, message.Id));
        }
        public void UpdateMessage(UserId modifiedByUser, MessageId messageId, string text, MediaContent? mediaContent)
        {
            if (modifiedByUser == null)
            {
                throw new ArgumentNullException();
            }
            var message = _messages.FirstOrDefault(x => x.Id == messageId);
            if (message == null)
            {
                throw new MessageNotFoundException("message is not found");
            }
            if (!String.IsNullOrWhiteSpace(text))
            {
                message.UpdateText(modifiedByUser, text);
            }
            if (mediaContent != null)
            {
                message.UpdateMediaContent(modifiedByUser, mediaContent);
            }
            Raise(new ChatUpdateMessageDomainEvent(this.Id, modifiedByUser, message.UserForeignKey, message.Id));
        }
        public void UpdatePicture(UserId modifiedBy, Picture picture)
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
            Raise(new ChatUpdatedDomainEvent(this.Id));
        }
        public void UpdateName(UserId modifiedBy, string name)
        {
            if (modifiedBy == null)
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
            this.Name = name;
            SetLastModified(modifiedBy);
            Raise(new ChatUpdatedDomainEvent(this.Id));
        }
        public void UpdateDescription(UserId modifiedBy, string text)
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
            this.Description = text;
            SetLastModified(modifiedBy);
            Raise(new ChatUpdatedDomainEvent(this.Id));
        }
        public void Remove(UserId deletedBy)
        {
            if (deletedBy == null)
            {
                throw new ArgumentNullException($"{nameof(deletedBy)} argument is null");
            }
            if (IsDeleted())
            {
                throw new ChatAlreadyDeletedException("chat is already deleted");
            }
            if (!IsChatAdmin(deletedBy))
            {
                throw new UserIsNoAdminInChatException("executive is no chat admin");
            }
            SetDeleted(deletedBy);
            Raise(new ChatDeletedDomainEvent(this.Id));
        }
        public ChatRelationToUser GetMember(UserId user)
        {
            if (user == null)
            {
                return null;
            }
            var chatMember = _members.Where(x => x.UserForeignKey == user).First();
            return chatMember;
        }
        public bool IsChatMember(UserId user)
        {
            var result = user != null && _members.Any() && _members.Where(x => x.UserForeignKey == user).Count() != 0;
            return result;
        }
        public bool IsChatAdmin(UserId user)
        {
            var result = user != null && _members.Any() && _members.Where(x => x.UserForeignKey == user && (x.IsChatAdmin ?? false)).Count() != 0;
            return result;
        }
        public ChatRelationToUser GetChatMemberById(UserId userId) => _members.Where(x => x.UserForeignKey == userId).Single();

        public bool IsDeleted() => this.DeletedTime != null;
        public void ClearAllMembers() => _members.Clear();

    }
    public sealed partial class Chat
    {

        public IReadOnlyCollection<ChatRelationToUser>? ChatRelationToUsers => _members; 
        public IReadOnlyCollection<ChatInviteRequest>? ChatInvitesToUsers => _invites; 
        public IReadOnlyCollection<Message> Messages => _messages.ToImmutableList(); 
    }
}
