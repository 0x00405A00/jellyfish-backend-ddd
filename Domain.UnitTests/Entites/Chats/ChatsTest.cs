﻿using Domain.Entities.Chats;
using Domain.Entities.Chats.Exceptions;
using Domain.Entities.Messages;
using Domain.Entities.Messages.Exceptions;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;

namespace Domain.UnitTests.Entites.Chats
{
    public class ChatTests
    {

        [Fact]
        public void Create_ValidParameters_CreatesChat()
        {
            // Arrange
            var chatId = new ChatId(Guid.NewGuid());
            var createdByUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chatName = "Test Chat";
            var chatDescription = "This is a test chat";
            Picture picture = null; // Setze das Bild, falls benötigt
            var members = new List<ChatRelationToUser>
            {
                ChatRelationToUser.Create(ChatRelationToUser.NewId(),SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance().Id,chatId,false,DateTime.UtcNow.ToTypedDateTime(),null,null,null,null,null),
                ChatRelationToUser.Create(ChatRelationToUser.NewId(),SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance().Id,chatId,false,DateTime.UtcNow.ToTypedDateTime(),null,null,null,null,null),
                ChatRelationToUser.Create(ChatRelationToUser.NewId(),createdByUser.Id,chatId,true,DateTime.UtcNow.ToTypedDateTime(),null,null,null,null,null),
            };
            var messages = new List<Message>();
            var createdTime = DateTime.UtcNow;
            DateTime? lastModifiedTime = null;
            DateTime? deletedTime = null;

            // Act
            var chat = Chat.Create(chatId,chatName,chatDescription,picture,members,createdTime.ToTypedDateTime(),createdByUser.Id,null,null,null,null);

            // Assert
            Assert.NotNull(chat);
        }

        [Fact]
        public void Create_LessThanTwoMembers_ThrowsChatCreateException()
        {
            // Arrange
            var chatId = new ChatId(Guid.NewGuid());
            var createdByUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chatName = "Test Chat";
            List<ChatRelationToUser> members = null; // Keine Mitglieder

            // Act and Assert
            var exception = Assert.Throws<ChatCreateException>(() => Chat.Create(chatId,chatName,null,null,members,DateTime.UtcNow.ToTypedDateTime(),createdByUser.Id,null,null,null,null));
            Assert.NotNull(exception);
        }

        [Fact]
        public void Create_NullMembers_ThrowsInvalidDataException()
        {
            // Arrange
            var chatId = new ChatId(Guid.NewGuid());
            var createdByUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chatName = "Test Chat";
            List<ChatRelationToUser> members = new List<ChatRelationToUser>()
            {
                ChatRelationToUser.Create(ChatRelationToUser.NewId(),SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance().Id,chatId,false,DateTime.UtcNow.ToTypedDateTime(),null,null,null,null,null),
                ChatRelationToUser.Create(ChatRelationToUser.NewId(),SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance().Id,chatId,false,DateTime.UtcNow.ToTypedDateTime(),null,null,null,null,null),
                null
            };

            // Act and Assert
            var exception = Assert.Throws<InvalidDataException>(() => Chat.Create(chatId, chatName, null, null, members, DateTime.UtcNow.ToTypedDateTime(), createdByUser.Id, null, null, null, null));
            Assert.NotNull(exception);
        }

        [Fact]
        public void Create_NullAdmins_ThrowsChatCreateException()
        {
            // Arrange
            var chatId = new ChatId(Guid.NewGuid());
            var createdByUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chatName = "Test Chat";
            List<ChatRelationToUser> members = new List<ChatRelationToUser>()
            {
                ChatRelationToUser.Create(ChatRelationToUser.NewId(),SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance().Id,chatId,false,DateTime.UtcNow.ToTypedDateTime(),null,null,null,null,null),
                ChatRelationToUser.Create(ChatRelationToUser.NewId(),SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance().Id,chatId,false,DateTime.UtcNow.ToTypedDateTime(),null,null,null,null,null),
                ChatRelationToUser.Create(ChatRelationToUser.NewId(),createdByUser.Id,chatId,false,DateTime.UtcNow.ToTypedDateTime(),null,null,null,null,null),
            };
            List<Entities.Users.User> admins = null; // Admins sind null

            // Act and Assert
            var exception = Assert.Throws<InvalidDataException>(() => Chat.Create(chatId, chatName,null,null,members,DateTime.UtcNow.ToTypedDateTime(), createdByUser.Id,null,null,null,null));
            Assert.NotNull(exception);
        }

        [Fact]
        public void AddMember_ValidInput_AddsMemberToChat()
        {
            // Arrange
            var chatId = Chat.NewId();
            var chatMemberUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chatMember = ChatRelationToUser.Create(ChatRelationToUser.NewId(), chatMemberUser.Id, chatId, false, DateTime.UtcNow.ToTypedDateTime(), null, null, null, null, null);
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var adminChatMember = ChatRelationToUser.Create(ChatRelationToUser.NewId(), adminUser.Id, chatId, true, DateTime.UtcNow.ToTypedDateTime(), null, null, null, null, null);
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser,chatId.Id);

            // Act
            chat.AddMember(adminUser.Id, chatMemberUser.Id);

            // Assert
            Assert.Contains(chatMemberUser.Id, chat.Members.Select(x => x.UserForeignKey).ToList());
        }

        [Fact]
        public void AddMember_NullUser_ThrowsArgumentNullException()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => chat.AddMember(adminUser.Id, null));
            Assert.NotNull(exception);
        }

        [Fact]
        public void AddMember_NotAdmin_ThrowsUserIsNoAdminInChatException()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var memberUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var nonAdminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);

            // Act
            var act = () => chat.AddMember(nonAdminUser.Id, memberUser.Id);
            // Assert
            var exception = Assert.Throws<UserIsNoAdminInChatException>(act);
            Assert.NotNull(exception);
        }

        [Fact]
        public void AddMember_AlreadyMember_ThrowsUserAlreadyMemberInChatException()
        {
            // Arrange
            var memberUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);

            // Act
            chat.AddMember(adminUser.Id, memberUser.Id); // Member already added
            var act = () => chat.AddMember(adminUser.Id, memberUser.Id);

            // Assert
            var exception = Assert.Throws<UserAlreadyMemberInChatException>(act);
            Assert.NotNull(exception);
        }

        [Fact]
        public void RemoveMember_ValidInput_RemovesMemberFromChat()
        {
            // Arrange
            var chatId = Chat.NewId();
            var chatMemberUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chatMember = ChatRelationToUser.Create(ChatRelationToUser.NewId(), chatMemberUser.Id, chatId, false, DateTime.UtcNow.ToTypedDateTime(), null, null, null, null, null);
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var adminChatMember = ChatRelationToUser.Create(ChatRelationToUser.NewId(), adminUser.Id, chatId, true, DateTime.UtcNow.ToTypedDateTime(), null, null, null, null, null);
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser, chatId.Id);

            // Act
            chat.AddMember(adminUser.Id, chatMemberUser.Id);
            chat.RemoveMember(adminUser.Id, chatMemberUser.Id);

            // Assert
            Assert.DoesNotContain(chatMemberUser, chat.Members.Select(x => x.User).ToList());
        }

        [Fact]
        public void RemoveMember_NullUser_ThrowsArgumentNullException()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);

            // Act
            var act = () => chat.RemoveMember(adminUser.Id, null);

            // Assert
            var exception = Assert.Throws<ArgumentNullException>(act);
            Assert.NotNull(exception);
        }

        [Fact]
        public void RemoveMember_NotAdmin_ThrowsUserIsNoAdminInChatException()
        {
            // Arrange
            var memberUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var nonAdminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();

            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);

            // Act
            var act = () => chat.RemoveMember(nonAdminUser.Id, memberUser.Id);

            // Assert
            var exception = Assert.Throws<UserIsNoAdminInChatException>(act);
            Assert.NotNull(exception);
        }

        [Fact]
        public void RemoveMember_NotMember_ThrowsUserIsNoMemberInChatException()
        {
            // Arrange
            var nonMemberUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();

            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);

            // Act
            var act = () => chat.RemoveMember(adminUser.Id, nonMemberUser.Id);

            // Assert
            var exception = Assert.Throws<UserIsNoMemberInChatException>(act);
        }
        [Fact]
        public void AssignAdmin_ValidUserAndAdminUser_AssignsAdmin()
        {
            // Arrange
            var chatId = Chat.NewId();
            var chatMemberUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chatMember = ChatRelationToUser.Create(ChatRelationToUser.NewId(), chatMemberUser.Id, chatId, false, DateTime.UtcNow.ToTypedDateTime(), null, null, null, null, null);
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var adminChatMember = ChatRelationToUser.Create(ChatRelationToUser.NewId(), adminUser.Id, chatId, true, DateTime.UtcNow.ToTypedDateTime(), null, null, null, null, null);
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser, chatId.Id);

            // Act
            chat.AddMember(adminUser.Id, chatMemberUser.Id);
            chat.AssignAdmin(adminUser.Id, chatMemberUser.Id);

            // Assert
            Assert.Contains(chatMemberUser.Id, chat.Admins.Select(x => x.UserForeignKey).ToList());
        }

        [Fact]
        public void AssignAdmin_NullCommandExecUser_ThrowsArgumentNullException()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var member = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => chat.AssignAdmin(null, member.Id));
            Assert.NotNull(exception);
        }

        [Fact]
        public void AssignAdmin_NullMember_ThrowsArgumentNullException()
        {
            // Arrange
            var commandExecUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(commandExecUser);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => chat.AssignAdmin(commandExecUser.Id, null));
            Assert.NotNull(exception);
        }

        [Fact]
        public void AssignAdmin_CommandExecUserNotAdmin_ThrowsUserIsNoAdminInChatException()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var member = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var otherUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();

            // Act and Assert
            chat.AddMember(adminUser.Id, member.Id);
            chat.AddMember(adminUser.Id, otherUser.Id);

            var exception = Assert.Throws<UserIsNoAdminInChatException>(() => chat.AssignAdmin(otherUser.Id, member.Id));
            Assert.NotNull(exception);

        }

        [Fact]
        public void AssignAdmin_MemberIsChatMember_ThrowsUserAlreadyMemberInChatException()
        {
            // Arrange
            var commandExecUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(commandExecUser);
            var member = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            // Act and Assert
            chat.AddMember(commandExecUser.Id, member.Id); // Füge das Mitglied dem Chat hinzu
            chat.AssignAdmin(commandExecUser.Id, member.Id);

            var exception = Assert.Throws<UserAlreadyAdminInChatException>(() => chat.AssignAdmin(commandExecUser.Id, member.Id));
            Assert.NotNull(exception);
        }

        [Fact]
        public void RevokeAdmin_ValidUserAndAdminUser_RevokesAdmin()
        {
            // Arrange
            var chatId = Chat.NewId();
            var chatMemberUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chatMember = ChatRelationToUser.Create(ChatRelationToUser.NewId(), chatMemberUser.Id, chatId, false, DateTime.UtcNow.ToTypedDateTime(), null, null, null, null, null);
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var adminChatMember = ChatRelationToUser.Create(ChatRelationToUser.NewId(), adminUser.Id, chatId, true, DateTime.UtcNow.ToTypedDateTime(), null, null, null, null, null);
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser, chatId.Id);

            // Act
            chat.AddMember(adminUser.Id, chatMemberUser.Id);
            chat.AssignAdmin(adminUser.Id, chatMemberUser.Id); // Weise den Admin zu
            chat.RevokeAdmin(adminUser.Id, chatMemberUser.Id);

            // Assert
            Assert.DoesNotContain(chatMemberUser, chat.Admins.Select(x => x.User).ToList());
        }

        [Fact]
        public void RevokeAdmin_NullCommandExecUser_ThrowsArgumentNullException()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var member = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();

            // Act and Assert
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => chat.RevokeAdmin(null, member.Id));
            Assert.NotNull(exception);
        }

        [Fact]
        public void RevokeAdmin_NullMember_ThrowsArgumentNullException()
        {
            // Arrange
            var commandExecUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(commandExecUser);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => chat.RevokeAdmin(commandExecUser.Id, null));
            Assert.NotNull(exception);
        }

        [Fact]
        public void RevokeAdmin_CommandExecUserNotAdmin_ThrowsUserIsNoAdminInChatException()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var commandExecUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var member = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            chat.AddMember(adminUser.Id, member.Id); // Füge das Mitglied dem Chat hinzu

            // Act and Assert
            var exception = Assert.Throws<UserIsNoAdminInChatException>(() => chat.RevokeAdmin(commandExecUser.Id, member.Id));
            Assert.NotNull(exception);
        }

        [Fact]
        public void RevokeAdmin_MemberIsNotChatMember_ThrowsUserIsNoMemberInChatException()
        {
            // Arrange
            var commandExecUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(commandExecUser);
            var member = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();

            // Act and Assert
            var exception = Assert.Throws<UserIsNoMemberInChatException>(() => chat.RevokeAdmin(commandExecUser.Id, member.Id));
            Assert.NotNull(exception);
        }
        [Fact]
        public void AddMessage_ValidOwnerAndText_AddsMessage()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var messageOwner = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var text = "Test message";

            // Act
            chat.AddMessage(messageOwner.Id, text, null);

            // Assert
            Assert.NotEmpty(chat.Messages);
        }

        public static string ValidMimeType = "image/jpeg";
        public static string InvalidMimeType = "images/jpeg";

        [Fact]
        public void AddMessage_ValidOwnerAndMediaContent_AddsMessage()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var messageOwner = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var mediaContent = MediaContent.Parse(new byte[8] {0,1,1,0,1,1,1,0 }, ValidMimeType);
            var text = "Test message with media";

            // Act
            chat.AddMessage(messageOwner.Id, text, mediaContent);

            // Assert
            Assert.NotEmpty(chat.Messages);
        }

        [Fact]
        public void AddMessage_NullOwner_ThrowsArgumentNullException()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var text = "Test message";

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => chat.AddMessage(null, text, null));
            Assert.NotNull(exception);
        }

        [Fact]
        public void AddMessage_EmptyTextAndNullMediaContent_ThrowsNotValidMessageException()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var messageOwner = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var text = string.Empty;

            // Act and Assert
            var exception = Assert.Throws<NotValidMessageException>(() => chat.AddMessage(messageOwner.Id, text, null));
            Assert.NotNull(exception);
        }
        [Fact]
        public void RemoveMessage_ValidDeletedByUserAndMessageId_RemovesMessage()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var messageOwner = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var message = chat.AddMessage(messageOwner.Id, "Test message", null); 
            var deletedByUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();

            // Act
            chat.RemoveMessage(deletedByUser.Id, message.Id);

            // Assert
            Assert.DoesNotContain(message, chat.Messages);
        }

        [Fact]
        public void RemoveMessage_MessageNotFound_ThrowsMessageNotFoundException()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var deletedByUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var invalidMessageId = Message.NewId();

            // Act and Assert
            var exception = Assert.Throws<MessageNotFoundException>(() => chat.RemoveMessage(deletedByUser.Id, invalidMessageId));
            Assert.NotNull(exception);
        }

        [Fact]
        public void RemoveMessage_NullDeletedByUser_ThrowsArgumentNullException()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var messageOwner = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var message = chat.AddMessage(messageOwner.Id, "Test message",null);
            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => chat.RemoveMessage(null, message.Id));
            Assert.NotNull(exception);
        }
        [Fact]
        public void UpdateMessage_ValidModifiedByUserAndMessageId_UpdatesTextAndMediaContent()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var messageOwner = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var message = chat.AddMessage(messageOwner.Id, "Test message", null); 
            var newText = "Updated text";
            var newMediaContent = MediaContent.Parse(new byte[8] { 0, 1, 1, 0, 1, 1, 1, 0 }, ValidMimeType);

            // Act
            chat.UpdateMessage(messageOwner.Id, message.Id, newText, newMediaContent);

            // Assert
            Assert.Equal(newText, message.Text);
            Assert.Equal(newMediaContent, message.MediaContent);
        }

        [Fact]
        public void UpdateMessage_MessageNotFound_ThrowsMessageNotFoundException()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var modifiedByUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var invalidMessageId = Message.NewId();

            // Act and Assert
            var exception = Assert.Throws<MessageNotFoundException>(() => chat.UpdateMessage(modifiedByUser.Id, invalidMessageId, "New text", null));
            Assert.NotNull(exception);
        }

        [Fact]
        public void UpdateMessage_ValidModifiedByUserAndMessageId_UpdatesText()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var messageOwner = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var message = chat.AddMessage(messageOwner.Id, "Test message", null); 
            var newText = "Updated text";

            // Act
            chat.UpdateMessage(messageOwner.Id, message.Id, newText, null);

            // Assert
            Assert.Equal(newText, message.Text);
            Assert.Null(message.MediaContent);
        }

        [Fact]
        public void UpdateMessage_ValidModifiedByUserAndMessageId_UpdatesMediaContent()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var messageOwner = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var message = chat.AddMessage(messageOwner.Id, "Test message", null); 
            var newMediaContent = MediaContent.Parse(new byte[8] { 0, 1, 1, 0, 1, 1, 1, 0 }, ValidMimeType);

            // Act
            chat.UpdateMessage(messageOwner.Id, message.Id, null, newMediaContent);

            // Assert
            Assert.Equal(newMediaContent, message.MediaContent);
        }

        [Fact]
        public void UpdateMessage_NullModifiedByUser_ThrowsArgumentNullException()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var messageOwner = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var message = chat.AddMessage(messageOwner.Id, "Test message", null);
            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => chat.UpdateMessage(null, message.Id, "New text", null));
            Assert.NotNull(exception);
        }
        [Fact]
        public void UpdatePicture_ValidAdminUser_UpdatesPicture()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var newPicture = Picture.Parse(new byte[8] { 0, 1, 1, 0, 1, 1, 1, 0 }, ValidMimeType);

            // Act
            chat.UpdatePicture(adminUser.Id, newPicture);

            // Assert
            Assert.Equal(newPicture, chat.Picture);
        }

        [Fact]
        public void UpdatePicture_NonAdminUser_ThrowsUserIsNoAdminInChatException()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var nonAdminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var newPicture = Picture.Parse(new byte[8] { 0, 1, 1, 0, 1, 1, 1, 0 }, ValidMimeType);

            // Act and Assert
            var exception = Assert.Throws<UserIsNoAdminInChatException>(() => chat.UpdatePicture(nonAdminUser.Id, (Picture)newPicture));
            Assert.NotNull(exception);
        }

        [Fact]
        public void UpdatePicture_NullAdminUser_ThrowsArgumentNullException()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var newPicture = Picture.Parse(new byte[8] { 0, 1, 1, 0, 1, 1, 1, 0 }, ValidMimeType);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => chat.UpdatePicture(null, newPicture));
            Assert.NotNull(exception);
        }
        [Fact]
        public void UpdateName_ValidAdminUserAndNonEmptyName_UpdatesChatName()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var newName = "NewChatName";

            // Act
            chat.UpdateName(adminUser.Id, newName);

            // Assert
            Assert.Equal(newName, chat.Name);
        }

        [Fact]
        public void UpdateName_NonAdminUser_ThrowsUserIsNoAdminInChatException()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var nonAdminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var newName = "NewChatName";

            // Act and Assert
            var exception = Assert.Throws<UserIsNoAdminInChatException>(() => chat.UpdateName(nonAdminUser.Id, newName));
            Assert.NotNull(exception);
        }

        [Fact]
        public void UpdateName_NullAdminUser_ThrowsArgumentNullException()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var newName = "NewChatName";

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => chat.UpdateName(null, newName));
            Assert.NotNull(exception);
        }

        [Fact]
        public void UpdateName_ValidAdminUserAndEmptyName_ThrowsNotValidChatDescriptionException()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var emptyName = string.Empty;

            // Act and Assert
            var exception = Assert.Throws<NotValidChatDescriptionException>(() => chat.UpdateName(adminUser.Id, emptyName));
            Assert.NotNull(exception);
        }
        [Fact]
        public void UpdateDescription_ValidAdminUserAndNonEmptyDescription_UpdatesChatDescription()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var newDescription = "NewChatDescription";

            // Act
            chat.UpdateDescription(adminUser.Id, newDescription);

            // Assert
            Assert.Equal(newDescription, chat.Description);
        }

        [Fact]
        public void UpdateDescription_NonAdminUser_ThrowsUserIsNoAdminInChatException()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var nonAdminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var newDescription = "NewChatDescription";

            // Act and Assert
            var exception = Assert.Throws<UserIsNoAdminInChatException>(() => chat.UpdateDescription(nonAdminUser.Id, newDescription));
            Assert.NotNull(exception);
        }

        [Fact]
        public void UpdateDescription_NullAdminUser_ThrowsArgumentNullException()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var newDescription = "NewChatDescription";

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => chat.UpdateDescription(null, newDescription));
            Assert.NotNull(exception);
        }

        [Fact]
        public void UpdateDescription_ValidAdminUserAndEmptyDescription_ThrowsNotValidChatDescriptionException()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var emptyDescription = string.Empty;

            // Act and Assert
            var exception = Assert.Throws<NotValidChatDescriptionException>(() => chat.UpdateDescription(adminUser.Id, emptyDescription));
            Assert.NotNull(exception);
        }
        [Fact]
        public void Remove_ValidAdminUserAndNotDeleted_ChatIsDeleted()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);

            // Act
            chat.Remove(adminUser.Id);

            // Assert
            Assert.True(chat.IsDeleted());
        }

        [Fact]
        public void Remove_AlreadyDeleted_ThrowsChatAlreadyDeletedException()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            chat.SetDeleted(SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance().Id);

            // Act and Assert
            var exception = Assert.Throws<ChatAlreadyDeletedException>(() => chat.Remove(adminUser.Id));
            Assert.NotNull(exception);
        }

        [Fact]
        public void Remove_NonAdminUser_ThrowsUserIsNoAdminInChatException()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);
            var nonAdminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();

            // Act and Assert
            var exception = Assert.Throws<UserIsNoAdminInChatException>(() => chat.Remove(nonAdminUser.Id));
            Assert.NotNull(exception);
        }

        [Fact]
        public void Remove_NullAdminUser_ThrowsArgumentNullException()
        {
            // Arrange
            var adminUser = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chat = SharedTest.DomainTestInstance.Entity.Chats.InstancingHelper.GetChatInstance(adminUser);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => chat.Remove(null)); 
            Assert.NotNull(exception);
        }

    }
}
