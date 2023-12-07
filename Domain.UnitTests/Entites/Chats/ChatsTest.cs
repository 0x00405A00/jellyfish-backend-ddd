using Domain.Entities.Chats;
using Domain.Entities.Chats.Exception;
using Domain.Entities.Message.Exception;
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
            var createdByUser = User.InstancingHelper.GetUserInstance();
            var chatName = "Test Chat";
            var chatDescription = "This is a test chat";
            Picture picture = null; // Setze das Bild, falls benötigt
            var members = new List<ChatMember>
            {
                ChatMember.Create(User.InstancingHelper.GetUserInstance(),false,DateTime.Now,null,null),
                ChatMember.Create(User.InstancingHelper.GetUserInstance(),false,DateTime.Now,null,null),
                ChatMember.Create(createdByUser,true,DateTime.Now,null,null),
            };
            var messages = new List<Domain.Entities.Message.Message>();
            var createdTime = DateTime.Now;
            DateTime? lastModifiedTime = null;
            DateTime? deletedTime = null;

            // Act
            var chat = Chat.Create(chatId, createdByUser, chatName, chatDescription, picture, members, messages, createdTime, lastModifiedTime, deletedTime);

            // Assert
            Assert.NotNull(chat);
        }

        [Fact]
        public void Create_LessThanTwoMembers_ThrowsChatCreateException()
        {
            // Arrange
            var chatId = new ChatId(Guid.NewGuid());
            var createdByUser = User.InstancingHelper.GetUserInstance();
            var chatName = "Test Chat";
            List<ChatMember> members = null; // Keine Mitglieder

            // Act and Assert
            var exception = Assert.Throws<ChatCreateException>(() => Chat.Create(chatId, createdByUser, chatName, null, null, members, null, DateTime.Now, null, null));
            Assert.NotNull(exception);
        }

        [Fact]
        public void Create_NullMembers_ThrowsInvalidDataException()
        {
            // Arrange
            var chatId = new ChatId(Guid.NewGuid());
            var createdByUser = User.InstancingHelper.GetUserInstance();
            var chatName = "Test Chat";
            List<ChatMember> members = new List<ChatMember>()
            { 
                ChatMember.Create(User.InstancingHelper.GetUserInstance(),false,DateTime.Now,null,null),
                ChatMember.Create(User.InstancingHelper.GetUserInstance(),false,DateTime.Now,null,null),
                null
            };

            // Act and Assert
            var exception = Assert.Throws<InvalidDataException>(() => Chat.Create(chatId, createdByUser, chatName, null, null, members, null, DateTime.Now, null, null));
            Assert.NotNull(exception);
        }

        [Fact]
        public void Create_NullAdmins_ThrowsChatCreateException()
        {
            // Arrange
            var chatId = new ChatId(Guid.NewGuid());
            var createdByUser = User.InstancingHelper.GetUserInstance();
            var chatName = "Test Chat";
            List<ChatMember> members = new List<ChatMember>()
            {
                ChatMember.Create(User.InstancingHelper.GetUserInstance(),false,DateTime.Now,null,null),
                ChatMember.Create(User.InstancingHelper.GetUserInstance(),false,DateTime.Now,null,null),
                ChatMember.Create(createdByUser,false,DateTime.Now,null,null),
            };
            List<Entities.User.User> admins = null; // Admins sind null

            // Act and Assert
            var exception = Assert.Throws<InvalidDataException>(() => Chat.Create(chatId, createdByUser, chatName, null, null, members, null, DateTime.Now, null, null));
            Assert.NotNull(exception);
        }

        [Fact]
        public void AddMember_ValidInput_AddsMemberToChat()
        {
            // Arrange
            var chatMemberUser = User.InstancingHelper.GetUserInstance();
            var chatMember = ChatMember.Create(chatMemberUser, false, DateTime.Now, null, null);
            var adminUser = User.InstancingHelper.GetUserInstance();
            var adminChatMember = ChatMember.Create(adminUser, true, DateTime.Now, null, null);
            var chat = InstancingHelper.GetChatInstance(adminUser);

            // Act
            chat.AddMember(adminUser, chatMemberUser);

            // Assert
            Assert.Contains(chatMemberUser, chat.Members.Select(x=>x.User).ToList());
        }

        [Fact]
        public void AddMember_NullUser_ThrowsArgumentNullException()
        {
            // Arrange
            var adminUser = Domain.UnitTests.Entites.User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => chat.AddMember(adminUser, null));
            Assert.NotNull(exception);
        }

        [Fact]
        public void AddMember_NotAdmin_ThrowsUserIsNoAdminInChatException()
        {
            // Arrange
            var adminUser = Domain.UnitTests.Entites.User.InstancingHelper.GetUserInstance();
            var memberUser = Domain.UnitTests.Entites.User.InstancingHelper.GetUserInstance();
            var nonAdminUser = Domain.UnitTests.Entites.User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);

            // Act
            var act = () => chat.AddMember(nonAdminUser, memberUser);
            // Assert
            var exception = Assert.Throws<UserIsNoAdminInChatException>(act);
            Assert.NotNull(exception);
        }

        [Fact]
        public void AddMember_AlreadyMember_ThrowsUserAlreadyMemberInChatException()
        {
            // Arrange
            var memberUser = Domain.UnitTests.Entites.User.InstancingHelper.GetUserInstance();
            var adminUser = Domain.UnitTests.Entites.User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);

            // Act
            chat.AddMember(adminUser, memberUser); // Member already added
            var act = () => chat.AddMember(adminUser, memberUser);

            // Assert
            var exception = Assert.Throws<UserAlreadyMemberInChatException>(act);
            Assert.NotNull(exception);
        }

        [Fact]
        public void RemoveMember_ValidInput_RemovesMemberFromChat()
        {
            // Arrange
            var chatMemberUser = User.InstancingHelper.GetUserInstance();
            var chatMember = ChatMember.Create(chatMemberUser, false, DateTime.Now, null, null);
            var adminUser = User.InstancingHelper.GetUserInstance();
            var adminChatMember = ChatMember.Create(adminUser, true, DateTime.Now, null, null);
            var chat = InstancingHelper.GetChatInstance(adminUser);


            // Act
            chat.AddMember(adminUser, chatMemberUser);
            chat.RemoveMember(adminUser, chatMemberUser);

            // Assert
            Assert.DoesNotContain(chatMemberUser, chat.Members.Select(x => x.User).ToList());
        }

        [Fact]
        public void RemoveMember_NullUser_ThrowsArgumentNullException()
        {
            // Arrange
            var adminUser = Domain.UnitTests.Entites.User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);

            // Act
            var act = () => chat.RemoveMember(adminUser, null);

            // Assert
            var exception = Assert.Throws<ArgumentNullException>(act);
            Assert.NotNull(exception);
        }

        [Fact]
        public void RemoveMember_NotAdmin_ThrowsUserIsNoAdminInChatException()
        {
            // Arrange
            var memberUser = Domain.UnitTests.Entites.User.InstancingHelper.GetUserInstance();
            var nonAdminUser = Domain.UnitTests.Entites.User.InstancingHelper.GetUserInstance();

            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);

            // Act
            var act = () => chat.RemoveMember(nonAdminUser, memberUser);

            // Assert
            var exception = Assert.Throws<UserIsNoAdminInChatException>(act);
            Assert.NotNull(exception);
        }

        [Fact]
        public void RemoveMember_NotMember_ThrowsUserIsNoMemberInChatException()
        {
            // Arrange
            var nonMemberUser = Domain.UnitTests.Entites.User.InstancingHelper.GetUserInstance();
            var adminUser = Domain.UnitTests.Entites.User.InstancingHelper.GetUserInstance();

            var chat = InstancingHelper.GetChatInstance(adminUser);

            // Act
            var act = () => chat.RemoveMember(adminUser, nonMemberUser);

            // Assert
            var exception = Assert.Throws<UserIsNoMemberInChatException>(act);
        }
        [Fact]
        public void AssignAdmin_ValidUserAndAdminUser_AssignsAdmin()
        {
            // Arrange
            var chatMemberUser = User.InstancingHelper.GetUserInstance();
            var chatMember = ChatMember.Create(chatMemberUser, false, DateTime.Now, null, null);
            var adminUser = User.InstancingHelper.GetUserInstance();
            var adminChatMember = ChatMember.Create(adminUser, true, DateTime.Now, null, null);
            var chat = InstancingHelper.GetChatInstance(adminUser);

            // Act
            chat.AddMember(adminUser, chatMemberUser);
            chat.AssignAdmin(adminUser, chatMemberUser);

            // Assert
            Assert.Contains(chatMemberUser, chat.Admins.Select(x => x.User).ToList());
        }

        [Fact]
        public void AssignAdmin_NullCommandExecUser_ThrowsArgumentNullException()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var member = Domain.UnitTests.Entites.User.InstancingHelper.GetUserInstance();

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => chat.AssignAdmin(null, member));
            Assert.NotNull(exception);
        }

        [Fact]
        public void AssignAdmin_NullMember_ThrowsArgumentNullException()
        {
            // Arrange
            var commandExecUser = Domain.UnitTests.Entites.User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(commandExecUser);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => chat.AssignAdmin(commandExecUser, null));
            Assert.NotNull(exception);
        }

        [Fact]
        public void AssignAdmin_CommandExecUserNotAdmin_ThrowsUserIsNoAdminInChatException()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var member = Domain.UnitTests.Entites.User.InstancingHelper.GetUserInstance();
            var otherUser = Domain.UnitTests.Entites.User.InstancingHelper.GetUserInstance();

            // Act and Assert
            chat.AddMember(adminUser, member);
            chat.AddMember(adminUser, otherUser);

            var exception = Assert.Throws<UserIsNoAdminInChatException>(() => chat.AssignAdmin(otherUser, member));
            Assert.NotNull(exception);

        }

        [Fact]
        public void AssignAdmin_MemberIsChatMember_ThrowsUserAlreadyMemberInChatException()
        {
            // Arrange
            var commandExecUser = Domain.UnitTests.Entites.User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(commandExecUser);
            var member = Domain.UnitTests.Entites.User.InstancingHelper.GetUserInstance();
            // Act and Assert
            chat.AddMember(commandExecUser, member); // Füge das Mitglied dem Chat hinzu
            chat.AssignAdmin(commandExecUser, member);

            var exception = Assert.Throws<UserAlreadyAdminInChatException>(() => chat.AssignAdmin(commandExecUser, member));
            Assert.NotNull(exception);
        }

        [Fact]
        public void RevokeAdmin_ValidUserAndAdminUser_RevokesAdmin()
        {
            // Arrange

            var chatMemberUser = User.InstancingHelper.GetUserInstance();
            var chatMember = ChatMember.Create(chatMemberUser, false, DateTime.Now, null, null);
            var adminUser = User.InstancingHelper.GetUserInstance();
            var adminChatMember = ChatMember.Create(adminUser, true, DateTime.Now, null, null);
            var chat = InstancingHelper.GetChatInstance(adminUser);

            // Act
            chat.AddMember(adminUser, chatMemberUser);
            chat.AssignAdmin(adminUser, chatMemberUser); // Weise den Admin zu
            chat.RevokeAdmin(adminUser, chatMemberUser);

            // Assert
            Assert.DoesNotContain(chatMemberUser, chat.Admins.Select(x => x.User).ToList());
        }

        [Fact]
        public void RevokeAdmin_NullCommandExecUser_ThrowsArgumentNullException()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var member = User.InstancingHelper.GetUserInstance();

            // Act and Assert
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => chat.RevokeAdmin(null, member));
            Assert.NotNull(exception);
        }

        [Fact]
        public void RevokeAdmin_NullMember_ThrowsArgumentNullException()
        {
            // Arrange
            var commandExecUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(commandExecUser);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => chat.RevokeAdmin(commandExecUser, null));
            Assert.NotNull(exception);
        }

        [Fact]
        public void RevokeAdmin_CommandExecUserNotAdmin_ThrowsUserIsNoAdminInChatException()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var commandExecUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var member = User.InstancingHelper.GetUserInstance();
            chat.AddMember(adminUser, member); // Füge das Mitglied dem Chat hinzu

            // Act and Assert
            var exception = Assert.Throws<UserIsNoAdminInChatException>(() => chat.RevokeAdmin(commandExecUser, member));
            Assert.NotNull(exception);
        }

        [Fact]
        public void RevokeAdmin_MemberIsNotChatMember_ThrowsUserIsNoMemberInChatException()
        {
            // Arrange
            var commandExecUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(commandExecUser);
            var member = User.InstancingHelper.GetUserInstance();

            // Act and Assert
            var exception = Assert.Throws<UserIsNoMemberInChatException>(() => chat.RevokeAdmin(commandExecUser, member));
            Assert.NotNull(exception);
        }
        [Fact]
        public void AddMessage_ValidOwnerAndText_AddsMessage()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var messageOwner = User.InstancingHelper.GetUserInstance();
            var text = "Test message";

            // Act
            chat.AddMessage(messageOwner, text, null);

            // Assert
            Assert.NotEmpty(chat.Messages);
        }

        public static string ValidMimeType = "image/jpeg";
        public static string InvalidMimeType = "images/jpeg";

        [Fact]
        public void AddMessage_ValidOwnerAndMediaContent_AddsMessage()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var messageOwner = User.InstancingHelper.GetUserInstance();
            var mediaContent = MediaContent.Parse(new byte[8] {0,1,1,0,1,1,1,0 }, ValidMimeType);
            var text = "Test message with media";

            // Act
            chat.AddMessage(messageOwner, text, mediaContent);

            // Assert
            Assert.NotEmpty(chat.Messages);
        }

        [Fact]
        public void AddMessage_NullOwner_ThrowsArgumentNullException()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var text = "Test message";

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => chat.AddMessage(null, text, null));
            Assert.NotNull(exception);
        }

        [Fact]
        public void AddMessage_EmptyTextAndNullMediaContent_ThrowsNotValidMessageException()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var messageOwner = User.InstancingHelper.GetUserInstance();
            var text = string.Empty;

            // Act and Assert
            var exception = Assert.Throws<NotValidMessageException>(() => chat.AddMessage(messageOwner, text, null));
            Assert.NotNull(exception);
        }
        [Fact]
        public void RemoveMessage_ValidDeletedByUserAndMessageId_RemovesMessage()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var messageOwner = User.InstancingHelper.GetUserInstance();
            var message = chat.AddMessage(messageOwner, "Test message", null); 
            var deletedByUser = User.InstancingHelper.GetUserInstance();

            // Act
            chat.RemoveMessage(deletedByUser, message.Uuid);

            // Assert
            Assert.DoesNotContain(message, chat.Messages);
        }

        [Fact]
        public void RemoveMessage_MessageNotFound_ThrowsMessageNotFoundException()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var deletedByUser = User.InstancingHelper.GetUserInstance();
            var invalidMessageId = new Domain.Entities.Message.MessageId(Guid.NewGuid());

            // Act and Assert
            var exception = Assert.Throws<MessageNotFoundException>(() => chat.RemoveMessage(deletedByUser, invalidMessageId));
            Assert.NotNull(exception);
        }

        [Fact]
        public void RemoveMessage_NullDeletedByUser_ThrowsArgumentNullException()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var messageOwner = User.InstancingHelper.GetUserInstance();
            var message = chat.AddMessage(messageOwner, "Test message",null);
            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => chat.RemoveMessage(null, message.Uuid));
            Assert.NotNull(exception);
        }
        [Fact]
        public void UpdateMessage_ValidModifiedByUserAndMessageId_UpdatesTextAndMediaContent()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var messageOwner = User.InstancingHelper.GetUserInstance();
            var message = chat.AddMessage(messageOwner, "Test message", null); 
            var newText = "Updated text";
            var newMediaContent = MediaContent.Parse(new byte[8] { 0, 1, 1, 0, 1, 1, 1, 0 }, ValidMimeType);

            // Act
            chat.UpdateMessage(messageOwner, message.Uuid, newText, newMediaContent);

            // Assert
            Assert.Equal(newText, message.Text);
            Assert.Equal(newMediaContent, message.MediaContent);
        }

        [Fact]
        public void UpdateMessage_MessageNotFound_ThrowsMessageNotFoundException()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var modifiedByUser = User.InstancingHelper.GetUserInstance();
            var invalidMessageId = new Domain.Entities.Message.MessageId(Guid.NewGuid());

            // Act and Assert
            var exception = Assert.Throws<MessageNotFoundException>(() => chat.UpdateMessage(modifiedByUser, invalidMessageId, "New text", null));
            Assert.NotNull(exception);
        }

        [Fact]
        public void UpdateMessage_ValidModifiedByUserAndMessageId_UpdatesText()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var messageOwner = User.InstancingHelper.GetUserInstance();
            var message = chat.AddMessage(messageOwner, "Test message", null); 
            var newText = "Updated text";

            // Act
            chat.UpdateMessage(messageOwner, message.Uuid, newText, null);

            // Assert
            Assert.Equal(newText, message.Text);
            Assert.Null(message.MediaContent);
        }

        [Fact]
        public void UpdateMessage_ValidModifiedByUserAndMessageId_UpdatesMediaContent()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var messageOwner = User.InstancingHelper.GetUserInstance();
            var message = chat.AddMessage(messageOwner, "Test message", null); 
            var newMediaContent = MediaContent.Parse(new byte[8] { 0, 1, 1, 0, 1, 1, 1, 0 }, ValidMimeType);

            // Act
            chat.UpdateMessage(messageOwner, message.Uuid, null, newMediaContent);

            // Assert
            Assert.Equal(newMediaContent, message.MediaContent);
        }

        [Fact]
        public void UpdateMessage_NullModifiedByUser_ThrowsArgumentNullException()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var messageOwner = User.InstancingHelper.GetUserInstance();
            var message = chat.AddMessage(messageOwner, "Test message", null);
            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => chat.UpdateMessage(null, message.Uuid, "New text", null));
            Assert.NotNull(exception);
        }
        [Fact]
        public void UpdatePicture_ValidAdminUser_UpdatesPicture()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var newPicture = Picture.Parse(new byte[8] { 0, 1, 1, 0, 1, 1, 1, 0 }, ValidMimeType);

            // Act
            chat.UpdatePicture(adminUser, newPicture);

            // Assert
            Assert.Equal(newPicture, chat.Picture);
        }

        [Fact]
        public void UpdatePicture_NonAdminUser_ThrowsUserIsNoAdminInChatException()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var nonAdminUser = User.InstancingHelper.GetUserInstance();
            var newPicture = Picture.Parse(new byte[8] { 0, 1, 1, 0, 1, 1, 1, 0 }, ValidMimeType);

            // Act and Assert
            var exception = Assert.Throws<UserIsNoAdminInChatException>(() => chat.UpdatePicture(nonAdminUser, (Picture)newPicture));
            Assert.NotNull(exception);
        }

        [Fact]
        public void UpdatePicture_NullAdminUser_ThrowsArgumentNullException()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var newPicture = Picture.Parse(new byte[8] { 0, 1, 1, 0, 1, 1, 1, 0 }, ValidMimeType);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => chat.UpdatePicture(null, newPicture));
            Assert.NotNull(exception);
        }
        [Fact]
        public void UpdateName_ValidAdminUserAndNonEmptyName_UpdatesChatName()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var newName = "NewChatName";

            // Act
            chat.UpdateName(adminUser, newName);

            // Assert
            Assert.Equal(newName, chat.ChatName);
        }

        [Fact]
        public void UpdateName_NonAdminUser_ThrowsUserIsNoAdminInChatException()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var nonAdminUser = User.InstancingHelper.GetUserInstance();
            var newName = "NewChatName";

            // Act and Assert
            var exception = Assert.Throws<UserIsNoAdminInChatException>(() => chat.UpdateName(nonAdminUser, newName));
            Assert.NotNull(exception);
        }

        [Fact]
        public void UpdateName_NullAdminUser_ThrowsArgumentNullException()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var newName = "NewChatName";

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => chat.UpdateName(null, newName));
            Assert.NotNull(exception);
        }

        [Fact]
        public void UpdateName_ValidAdminUserAndEmptyName_ThrowsNotValidChatDescriptionException()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var emptyName = string.Empty;

            // Act and Assert
            var exception = Assert.Throws<NotValidChatDescriptionException>(() => chat.UpdateName(adminUser, emptyName));
            Assert.NotNull(exception);
        }
        [Fact]
        public void UpdateDescription_ValidAdminUserAndNonEmptyDescription_UpdatesChatDescription()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var newDescription = "NewChatDescription";

            // Act
            chat.UpdateDescription(adminUser, newDescription);

            // Assert
            Assert.Equal(newDescription, chat.ChatDescription);
        }

        [Fact]
        public void UpdateDescription_NonAdminUser_ThrowsUserIsNoAdminInChatException()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var nonAdminUser = User.InstancingHelper.GetUserInstance();
            var newDescription = "NewChatDescription";

            // Act and Assert
            var exception = Assert.Throws<UserIsNoAdminInChatException>(() => chat.UpdateDescription(nonAdminUser, newDescription));
            Assert.NotNull(exception);
        }

        [Fact]
        public void UpdateDescription_NullAdminUser_ThrowsArgumentNullException()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var newDescription = "NewChatDescription";

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => chat.UpdateDescription(null, newDescription));
            Assert.NotNull(exception);
        }

        [Fact]
        public void UpdateDescription_ValidAdminUserAndEmptyDescription_ThrowsNotValidChatDescriptionException()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var emptyDescription = string.Empty;

            // Act and Assert
            var exception = Assert.Throws<NotValidChatDescriptionException>(() => chat.UpdateDescription(adminUser, emptyDescription));
            Assert.NotNull(exception);
        }
        [Fact]
        public void Remove_ValidAdminUserAndNotDeleted_ChatIsDeleted()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);

            // Act
            chat.Remove(adminUser);

            // Assert
            Assert.True(chat.IsDeleted());
        }

        [Fact]
        public void Remove_AlreadyDeleted_ThrowsChatAlreadyDeletedException()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            chat.SetDeleted(User.InstancingHelper.GetUserInstance());

            // Act and Assert
            var exception = Assert.Throws<ChatAlreadyDeletedException>(() => chat.Remove(adminUser));
            Assert.NotNull(exception);
        }

        [Fact]
        public void Remove_NonAdminUser_ThrowsUserIsNoAdminInChatException()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);
            var nonAdminUser = User.InstancingHelper.GetUserInstance();

            // Act and Assert
            var exception = Assert.Throws<UserIsNoAdminInChatException>(() => chat.Remove(nonAdminUser));
            Assert.NotNull(exception);
        }

        [Fact]
        public void Remove_NullAdminUser_ThrowsArgumentNullException()
        {
            // Arrange
            var adminUser = User.InstancingHelper.GetUserInstance();
            var chat = InstancingHelper.GetChatInstance(adminUser);

            // Act and Assert
            var exception = Assert.Throws<ArgumentNullException>(() => chat.Remove(null)); 
            Assert.NotNull(exception);
        }

    }
}
