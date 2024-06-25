using Domain.Const;
using Domain.Entities.Chats;
using Domain.Entities.Messages;
using Domain.Entities.Users;
using Domain.Extension;
using Domain.Primitives;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Shared.Infrastructure.EFCore
{
    public static class SampleDataSeedExtension
    {
        private static User RootUser = User.GetSystemUser();
        public struct SampleUserIds
        {
            public static UserId DarthVader = Guid.Parse("c3f257ff-1c85-4e3c-b6b2-21ef9afba21f").ToIdentification<UserId>();
            public static UserId DarthMaul = Guid.Parse("c3f257ff-1c85-4e3c-b6b2-21ef8afba31f").ToIdentification<UserId>();
            public static UserId LaiaOrgana = Guid.Parse("c3f257ff-1c85-4e3c-b6b2-21ef7afba41f").ToIdentification<UserId>();
            public static UserId LukeSkywalker = Guid.Parse("c3f257ff-1c85-4e3c-b6b2-21ef2afba51f").ToIdentification<UserId>();
            public static UserId JabbaTheHut = Guid.Parse("c3f257ff-1c85-4e3c-b6b2-21ef3afba61f").ToIdentification<UserId>();
            public static UserId CarlJohnson = Guid.Parse("c3f257ff-1c85-4e3c-b6b2-21ef5afba61f").ToIdentification<UserId>();
        }
        public struct SampleChatIds
        {
            public static ChatId GroupChatWith4Members = Guid.Parse("c3f257ff-1c95-4e3c-b6b2-21ef9afba21f").ToIdentification<ChatId>();
            public static ChatId PrivateChatWithTwoMembers = Guid.Parse("c3f257ff-1c86-4e3c-b6b2-21ef8afba31f").ToIdentification<ChatId>();
        }
        private static List<(UserId, string, string, string, Picture, string, List<UserId>, List<UserId>)> _sampleUsers = new List<(UserId, string, string, string, Picture, string, List<UserId>, List<UserId>)>()
        {
            (SampleUserIds.DarthVader,"darth-vader","Darth","Vader",null,"+49175123451",new List<UserId>(){ SampleUserIds.DarthMaul,SampleUserIds.JabbaTheHut },new List<UserId>()),
            (SampleUserIds.DarthMaul, "darth-maul", "Darth", "Maul",null,"+49175123452",new List<UserId>(),new List<UserId>()),
            (SampleUserIds.LaiaOrgana, "princess-laia", "Laia", "Organa",null,"+49175123453",new List<UserId>(){ SampleUserIds.LukeSkywalker,SampleUserIds.CarlJohnson },new List<UserId>()),
            (SampleUserIds.LukeSkywalker, "luke-skywalker", "Luke", "Skywalker", null, "+49175123454", new List < UserId >() { }, new List < UserId >() {SampleUserIds.DarthVader }),
            (SampleUserIds.JabbaTheHut, "jabba-the-hut", "Jabba", "TheHut", null, "+49175123455", new List < UserId >() { }, new List < UserId >() { }),
            (SampleUserIds.CarlJohnson, "carl-johnson", "Carl", "Johnson", null, "+49175123456", new List < UserId >() { }, new List < UserId >() { }),
        };
        private static List<(ChatId, string, string, Picture, List<UserId>)> _sampleChats = new List<(ChatId, string, string, Picture, List<UserId>)>()
        {
            (SampleChatIds.PrivateChatWithTwoMembers,"Private Chat","Chatdesc",null,new List<UserId>(3)
            { 
                SampleUserIds.DarthVader,
                SampleUserIds.DarthMaul, 
                UserConst.RootUserId.ToIdentification<UserId>()
            }),
            (SampleChatIds.GroupChatWith4Members,"Our Groupchat","Against the republic",null,new List<UserId>(5){ 
                UserConst.RootUserId.ToIdentification<UserId>(),
                SampleUserIds.DarthVader,
                SampleUserIds.DarthMaul,
                SampleUserIds.LukeSkywalker,
                SampleUserIds.LaiaOrgana 
            }),
        };
        public static Chat CreateSampleChat(
            ChatId chatId,
            UserId ownerId,
            string chatName,
            string chatDescription,
            List<ChatRelationToUser> chatRelationToUsers,
            Picture pictureObj = null)
        {
            int hashCode = new object().GetHashCode() * 41;
            int randNumber = Random.Shared.Next(1, 40);
            string name = chatName;
            string description = chatDescription;

            Picture? picture = pictureObj;
            List<ChatRelationToUser> members = chatRelationToUsers;
            CustomDateTime createdDateTime = DateTime.UtcNow.ToTypedDateOnly();
            UserId createdBy = ownerId;
            CustomDateTime? modifiedDateTime = null;
            UserId? modifiedBy = null;
            CustomDateTime? deletedDateTime = null;
            UserId? deletedBy = null;

            var chat = Chat.Create(
                chatId,
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
            chat.ClearAllMembers();
            return chat;
        }
        public static Message CreateSampleMessage(
            MessageId messageId,
            UserId messageOwner,
            ChatId chatId,
            UserId userId,
            string text,
            MediaContent mediaContent = null)
        {
            int hashCode = new object().GetHashCode() * 41;
            int randNumber = Random.Shared.Next(1, 40);


            MediaContent? media = mediaContent;
            CustomDateTime createdDateTime = DateTime.UtcNow.AddDays(-randNumber).ToTypedDateOnly();
            UserId createdBy = messageOwner;
            CustomDateTime? modifiedDateTime = null;
            UserId? modifiedBy = null;
            CustomDateTime? deletedDateTime = null;
            UserId? deletedBy = null;

            var msg = Message.Create(
                messageId,
                chatId,
                userId,
                text,
                media,
                createdDateTime,
                createdBy,
                modifiedDateTime,
                modifiedBy,
                deletedDateTime,
                deletedBy);

            return msg;
        }
        public static User CreateSampleUser(
            UserId userId,
            string userNameStr,
            string firstNameStr,
            string lastNameStr,
            string phoneNumberStr,
            Picture pictureObj = null)
        {
            int hashCode = new object().GetHashCode() * 41;
            int randNumber = Random.Shared.Next(1, 40);

            var userType = UserTypeId.GetUserType();
            string userName = userNameStr;
            string password = Password.GeneratePassword();
            string firstName = firstNameStr;
            string lastName = lastNameStr;
            string activationToken = null;
            string activationCode = null;
            string passwordResetCode = null;
            string passwordResetToken = null;
            CustomDateTime? passwordResetExpiresIn = null;
            Email email = Email.Parse($"{lastName}.{firstName}.sample@web.net");
            PhoneNumber phone = PhoneNumber.Parse(phoneNumberStr);
            Picture? picture = pictureObj;
            CustomDateTime dateOfBirth = User.MinimumBirthDayDate.AddYears(-(randNumber + User.MinimumAgeForRegistration)).ToTypedDateTime();
            CustomDateTime? activationDateTime = DateTime.UtcNow.ToTypedDateOnly();
            CustomDateTime createdDateTime = DateTime.UtcNow.ToTypedDateOnly();
            UserId createdBy = RootUser.Id;
            CustomDateTime? modifiedDateTime = null;
            UserId? modifiedBy = null;
            CustomDateTime? deletedDateTime = null;
            UserId? deletedBy = null;

            var user = User.Create(
                userId,
                userType,
                userName,
                password,
                firstName,
                lastName,
                activationToken,
                activationCode,
                passwordResetCode,
                passwordResetToken,
                passwordResetExpiresIn,
                email,
                phone,
                picture,
                null,
                dateOfBirth,
                activationDateTime,
                createdDateTime,
                createdBy,
                modifiedDateTime,
                modifiedBy,
                deletedDateTime,
                deletedBy);

            return user;
        }
        public static ModelBuilder SampleUsers(this ModelBuilder modelBuilder)
        {
            List<User> users = new List<User>();
            List<UserHasRelationToRole> userHasRelationToRole = new List<UserHasRelationToRole>();
            List<UserHasRelationToFriend> _friends = new List<UserHasRelationToFriend>();
            List<FriendshipRequest> _friendshipRequests = new List<FriendshipRequest>();

            foreach (var userDataTuple in _sampleUsers)
            {
                List<UserHasRelationToFriend> friends = new List<UserHasRelationToFriend>();
                List<FriendshipRequest> friendshipRequests = new List<FriendshipRequest>();

                var user = CreateSampleUser(
                    userDataTuple.Item1,
                    userDataTuple.Item2,
                    userDataTuple.Item3,
                    userDataTuple.Item4,
                    userDataTuple.Item6,
                    userDataTuple.Item5);

                var userRole = UserHasRelationToRole.NewUser(user.Id);

                foreach (var friend in userDataTuple.Item7)
                {
                    var id = UserHasRelationToFriend.NewId();
                    var createdDateTime = DateTime.UtcNow.ToTypedDateTime();
                    var friendObj = UserHasRelationToFriend.Create(id, user.Id, friend, createdDateTime, user.Id, null, null, null, null);
                    friends.Add(friendObj);
                }
                foreach (var friendshipRequest in userDataTuple.Item8)
                {
                    var id = FriendshipRequest.NewId();
                    var createdDateTime = DateTime.UtcNow.ToTypedDateTime();
                    var friend = FriendshipRequest.Create(id, "do you want to be my friend?", user.Id, friendshipRequest, createdDateTime, null, null);
                    friendshipRequests.Add(friend);
                }

                users.Add(user);
                userHasRelationToRole.Add(userRole);
                _friendshipRequests.AddRange(friendshipRequests);
                _friends.AddRange(friends);
            }

            modelBuilder.Entity<User>()
                .HasData(users);
            modelBuilder.Entity<UserHasRelationToRole>()
                .HasData(userHasRelationToRole);
            modelBuilder.Entity<UserHasRelationToFriend>()
                .HasData(_friends);
            modelBuilder.Entity<FriendshipRequest>()
                .HasData(_friendshipRequests);

            return modelBuilder;
        }
        public static ModelBuilder SampleChats(this ModelBuilder modelBuilder)
        {
            List<Chat> chats = new List<Chat>();
            List<ChatRelationToUser> chatRelationToUsers = new List<ChatRelationToUser>();
            List<Message> chatMessages = new List<Message>();

            foreach (var chatDataTuple in _sampleChats)
            {
                ChatId chatId = chatDataTuple.Item1;
                UserId owner = chatDataTuple.Item5.First();
                List<ChatRelationToUser> chatMembers = new List<ChatRelationToUser>();
                List<ChatRelationToUser> tmp = new List<ChatRelationToUser>();
                List<Message> chatMessage = new List<Message>();
                foreach (var memberTuple in chatDataTuple.Item5)
                {
                    ChatRelationToUser chatRelationToUser = null;
                    if (memberTuple == chatDataTuple.Item5.First())
                    {
                        chatRelationToUser = ChatRelationToUser.NewAdmin(
                            chatId,
                            memberTuple,
                            owner);
                    }
                    else
                    {
                        chatRelationToUser = ChatRelationToUser.NewMember(
                            chatId,
                            memberTuple,
                            owner);
                    }
                    if (chatMembers.Any(x => x.UserForeignKey == chatRelationToUser.UserForeignKey && x.ChatForeignKey == chatRelationToUser.ChatForeignKey))
                        throw new Exception("user is already a member in this chat");

                    chatMembers.Add(chatRelationToUser);
                    tmp.Add(chatRelationToUser);
                }
                var chat = CreateSampleChat(
                    chatId,
                    owner,
                    chatDataTuple.Item2,
                    chatDataTuple.Item3,
                    tmp,
                    chatDataTuple.Item4);

                var randomMember = Random.Shared.Next(0, chatMembers.Count);
                var messageOwner = chatMembers[randomMember];
                for (int i = 0; i < 100; i++)
                {
                    foreach (var memberTuple in chatDataTuple.Item5)
                    {
                        string messageOwnerName = "Root";
                        var selectedOwnerByRandom = _sampleUsers.Where(x => x.Item1 == messageOwner.UserForeignKey);
                        if(selectedOwnerByRandom.Any())
                        {
                            messageOwnerName = selectedOwnerByRandom.First().Item2;
                        }
                        var messageId = Message.NewId();
                        string text = $"Hey i want to test jellyfish with this message, this messsage will repeat by all members! :) This is message with iteration level {i}. I am {messageOwnerName}";

                        var message = CreateSampleMessage(
                            messageId,
                            messageOwner.UserForeignKey,
                            chatId,
                            memberTuple,
                            text);

                        chatMessage.Add(message);
                    }
                }

                chats.Add(chat);
                chatRelationToUsers.AddRange(chatMembers);
                chatMessages.AddRange(chatMessage);
            }

            modelBuilder.Entity<Chat>()
                .HasData(chats);

            modelBuilder.Entity<ChatRelationToUser>()
                .HasData(chatRelationToUsers);
            modelBuilder.Entity<Message>()
                .HasData(chatMessages);
            return modelBuilder;
        }
    }
}
