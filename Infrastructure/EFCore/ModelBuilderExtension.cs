using Domain.Const;
using Domain.Entities.Chats;
using Domain.Entities.Mails;
using Domain.Entities.Messages;
using Domain.Entities.Users;
using Domain.Extension;
using Domain.Primitives;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.EFCore.DatabaseEntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Shared.Linq;
using System;

namespace Infrastructure.EFCore
{
    internal static class ModelBuilderExtension
    {
        private static bool _isSampleDataLoaded = false;
        public static ModelBuilder CreateModels(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserHasRelationToFriendsConfiguration());
            modelBuilder.ApplyConfiguration(new FriendshipRequestsConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserHasRelationToRoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AuthConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
            modelBuilder.ApplyConfiguration(new ChatRelationToUserConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new MessageOutboxConfiguration());
            modelBuilder.ApplyConfiguration(new ChatInviteRequestConfiguration());
            modelBuilder.ApplyConfiguration(new EmailSendingTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MailOutboxConfiguration());
            modelBuilder.ApplyConfiguration(new MailOutboxRecipientConfiguration());
            modelBuilder.ApplyConfiguration(new MailOutboxAttachmentConfiguration());


            /*modelBuilder.Entity<Email>()
                .HasNoKey() // keyless
                .ToView(null);*/
            return modelBuilder;
        }
        public static ModelBuilder CreateInitialDataSeed(this ModelBuilder modelBuilder)
        {
            modelBuilder.CreateUserTypes();
            modelBuilder.CreateSystemUser();
            modelBuilder.CreateEmailSendingTypes();

            return modelBuilder;
        }
        public static ModelBuilder CreateSampleData(this ModelBuilder modelBuilder)
        {
            _isSampleDataLoaded = true;
            modelBuilder.SampleUsers();
            modelBuilder.SampleChats();
            return modelBuilder;
        }
        public static ModelBuilder CreateOwnedTypes(this ModelBuilder modelBuilder)
        {

            return modelBuilder;
        }
        public static bool IsSampleDataLoaded()=> _isSampleDataLoaded;
    }

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
        private static List<(UserId, string, string, string, Picture, string, List<UserId>, List<UserId>)> _sampleUsers = new List<(UserId,string,string,string,Picture, string, List<UserId>, List<UserId>)>()
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
            (SampleChatIds.PrivateChatWithTwoMembers,"Private Chat","Chatdesc",null,new List<UserId>(2){ SampleUserIds.DarthVader,SampleUserIds.DarthMaul}),
            (SampleChatIds.GroupChatWith4Members,"Our Groupchat","Against the republic",null,new List<UserId>(4){SampleUserIds.DarthVader,SampleUserIds.DarthMaul,SampleUserIds.LukeSkywalker,SampleUserIds.LaiaOrgana }),
        };
        public static Chat CreateSampleChat(
            ChatId chatId,
            UserId ownerId,
            string chatName,
            string chatDescription,
            List<ChatRelationToUser> chatRelationToUsers,
            Picture pictureObj = null)
        {
            int hashCode = (new object().GetHashCode()) * 41;
            int randNumber = Random.Shared.Next(1, 40);
            string name = chatName;
            string description = chatDescription;

            Domain.ValueObjects.Picture? picture = pictureObj;
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
            ChatId chatId,
            UserId userId,
            string text,
            MediaContent mediaContent = null)
        {
            int hashCode = (new object().GetHashCode()) * 41;
            int randNumber = Random.Shared.Next(1, 40);


            Domain.ValueObjects.MediaContent? media = mediaContent;
            CustomDateTime createdDateTime = DateTime.UtcNow.ToTypedDateOnly();
            UserId createdBy = RootUser.Id;
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
            int hashCode = (new object().GetHashCode()) * 41;
            int randNumber = Random.Shared.Next(1,40) ;

            var userType = UserTypeId.GetUserType();
            string userName = userNameStr;
            string password = Domain.ValueObjects.Password.GeneratePassword();
            string firstName = firstNameStr;
            string lastName = lastNameStr;
            string activationToken = null;
            string activationCode = null;
            string passwordResetCode = null;
            string passwordResetToken = null;
            CustomDateTime? passwordResetExpiresIn = null;
            Domain.ValueObjects.Email email = Email.Parse($"{lastName}.{firstName}.sample@web.net");
            Domain.ValueObjects.PhoneNumber phone = PhoneNumber.Parse(phoneNumberStr);
            Domain.ValueObjects.Picture? picture = pictureObj;
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
                    var friendObj = UserHasRelationToFriend.Create(id,user.Id,friend,createdDateTime,user.Id,null,null,null,null);
                    friends.Add(friendObj);
                }
                foreach (var friendshipRequest in userDataTuple.Item8)
                {
                    var id = FriendshipRequest.NewId();
                    var createdDateTime = DateTime.UtcNow.ToTypedDateTime();
                    var friend = FriendshipRequest.Create(id,"do you want to be my friend?",user.Id,friendshipRequest,createdDateTime,null,null);
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

                for(int i = 0;i<100;i++)
                {
                    foreach (var memberTuple in chatDataTuple.Item5)
                    {
                        var messageId = Message.NewId();
                        string text = $"Hey i want to test jellyfish with this message, this messsage will repeat by all members! :)";

                        var message = CreateSampleMessage(
                            messageId,
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
    public static class DataSeedExtension
    {
        public static ModelBuilder CreateEmailSendingTypes(this ModelBuilder modelBuilder)
        {
            var toType = EmailSendingType.Create(
                new EmailTypeId(EmailConst.Type.To),
                "to",
                CustomDateTime.Now(),
                null,
                null);

            var ccType = EmailSendingType.Create(
                new EmailTypeId(EmailConst.Type.Cc),
                "cc",
                CustomDateTime.Now(),
                null,
                null);

            var bccType = EmailSendingType.Create(
                new EmailTypeId(EmailConst.Type.Bcc),
                "bcc",
                CustomDateTime.Now(),
                null,
                null);

            modelBuilder.Entity<EmailSendingType>()
                .HasData(toType, ccType, bccType);

            return modelBuilder;
        }
        public static ModelBuilder CreateSystemUser(this ModelBuilder modelBuilder) 
        {
            List<User> users = new List<User>();
            var rootUser = User.GetSystemUser();
            var rootRole = UserHasRelationToRole.NewRoot(rootUser.Id);
            var adminRole = UserHasRelationToRole.NewAdmin(rootUser.Id);
            var userRole = UserHasRelationToRole.NewUser(rootUser.Id);

            users.Add(rootUser);
            modelBuilder.Entity<User>()
                .HasData(users);
            modelBuilder.Entity<UserHasRelationToRole>()
                .HasData(rootRole, adminRole, userRole);

            return modelBuilder;    
        }
        public static ModelBuilder CreateDbFunctions(this ModelBuilder modelBuilder)
        {

            /*modelBuilder.HasDbFunction(() => CustomDbFunctions.EmailContains(default, default)).HasTranslation(args =>
            {
                SqlExpression emailValue = args.First();
                SqlExpression shouldContainInEmail = args.Skip(1).First();
                SqlParameterExpression sqlParameterExpression = emailValue as SqlParameterExpression;

                var emailValueLiteral = emailValue.TypeMapping.GenerateSqlLiteral(emailValue.Print());
                var shouldContainInEmailLiteral = shouldContainInEmail.TypeMapping.GenerateSqlLiteral(shouldContainInEmail.Print());

                return new SqlFunctionExpression(
                    "emailcontains",
                    new List<SqlExpression>() { new SqlFragmentExpression(emailValueLiteral),new SqlFragmentExpression(shouldContainInEmailLiteral) },
                    false,
                    new List<bool>() { false, false, false },
                    typeof(DateTimeOffset),
                    null);
            });*/
            /*modelBuilder.HasDbFunction(typeof(CustomDbFunctions).GetMethod(nameof(CustomDbFunctions.EmailContains), new[] { typeof(string), typeof(string) }))
                .HasTranslation(
                    args =>
                        new SqlFragmentExpression(ExpressionType.));*/
            /*modelBuilder.HasDbFunction(typeof(CustomDbFunctions).GetMethod(nameof(CustomDbFunctions.PhoneNumberContains)),
            b =>
            {
                b.HasName("PhoneNumberContains");
                b.HasParameter("phoneNumber").PropagatesNullability();
                b.HasParameter("value").PropagatesNullability();
            });*/

            return modelBuilder;
        }
        public static ModelBuilder CreateUserTypes(this ModelBuilder modelBuilder)
        {
            var userType1 = UserType.Create(
                new UserTypeId(UserConst.UserType.User),
                UserConst.UserType.UserName,
                CustomDateTime.Now(),
                null,
                null,
                null,
                null,
                null);

            var userType2 = UserType.Create(
                new UserTypeId(UserConst.UserType.Admin),
                UserConst.UserType.AdminName,
                CustomDateTime.Now(),
                null,
                null,
                null,
                null,
                null);

            var userType3 = UserType.Create(
                new UserTypeId(UserConst.UserType.Root),
                UserConst.UserType.RootName,
                CustomDateTime.Now(),
                null,
                null,
                null,
                null,
                null);

            modelBuilder.Entity<UserType>()
                .HasData(userType1, userType2, userType3);

            return modelBuilder;
        }
    }
}
