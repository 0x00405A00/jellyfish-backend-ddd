using Domain.Const;
using Domain.Entities.Mails;
using Domain.Entities.Users;
using Domain.Primitives.Ids;
using Domain.Primitives;
using Infrastructure.EFCore.DatabaseEntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Domain.Extension;
using Shared.ApiDataTransferObject;
using Domain.ValueObjects;

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
        public static bool IsSampleDataLoaded()=> _isSampleDataLoaded;
    }

    public static class SampleDataSeedExtension
    {
        private static User RootUser = User.GetSystemUser();
        private static List<(UserId, string, string, string, Picture)> _sampleUsers = new List<(UserId,string,string,string,Picture)>()
        {
            (Guid.Parse("c3f257ff-1c85-4e3c-b6b2-21ef9afba21f").ToIdentification<UserId>(),"darth-vader","Darth","Vader",null),
            (Guid.Parse("c3f257ff-1c85-4e3c-b6b2-21ef8afba31f").ToIdentification < UserId >(), "darth-maul", "Darth", "Maul",null),
            (Guid.Parse("c3f257ff-1c85-4e3c-b6b2-21ef7afba41f").ToIdentification < UserId >(), "princess-laia", "Laia", "Organa",null),
            (Guid.Parse("c3f257ff-1c85-4e3c-b6b2-21ef2afba51f").ToIdentification < UserId >(), "luke-skywalker", "Luke", "Skywalker",null),
            (Guid.Parse("c3f257ff-1c85-4e3c-b6b2-21ef3afba61f").ToIdentification < UserId >(), "jabba-the-hut", "Jabba", "TheHut",null),
            (Guid.Parse("c3f257ff-1c85-4e3c-b6b2-21ef5afba61f").ToIdentification < UserId >(), "carl-johnson", "Carl", "Johnson",null),
        };
        public static User CreateSampleUser(UserId userId,string userNameStr,string firstNameStr,string lastNameStr,Picture pictureObj = null)
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
            Domain.ValueObjects.PhoneNumber phone = PhoneNumber.Parse($"+49175123{((randNumber).ToString().PadLeft(4,'0'))}");
            Domain.ValueObjects.Picture? picture = pictureObj;
            CustomDateTime dateOfBirth = User.MinimumBirthDayDate.AddYears(-(randNumber + User.MinimumAgeForRegistration)).ToTypedDateTime();
            CustomDateTime? activationDateTime = DateTime.Now.ToTypedDateOnly();
            CustomDateTime createdDateTime = DateTime.Now.ToTypedDateOnly();
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

            foreach(var userDataTuple in _sampleUsers)
            {
                var user = CreateSampleUser(userDataTuple.Item1, userDataTuple.Item2, userDataTuple.Item3, userDataTuple.Item4, userDataTuple.Item5);
                var userRole = UserHasRelationToRole.NewUser(user.Id);

                users.Add(user);
                userHasRelationToRole.Add(userRole);
            }

            modelBuilder.Entity<User>()
                .HasData(users);
            modelBuilder.Entity<UserHasRelationToRole>()
                .HasData(userHasRelationToRole);

            return modelBuilder;
        }
        public static ModelBuilder SampleChats(this ModelBuilder modelBuilder)
        {

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
                new CustomDateTime(DateTime.Now),
                null,
                null);

            var ccType = EmailSendingType.Create(
                new EmailTypeId(EmailConst.Type.Cc),
                "cc",
                new CustomDateTime(DateTime.Now),
                null,
                null);

            var bccType = EmailSendingType.Create(
                new EmailTypeId(EmailConst.Type.Bcc),
                "bcc",
                new CustomDateTime(DateTime.Now),
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
        public static ModelBuilder CreateUserTypes(this ModelBuilder modelBuilder)
        {
            var userType1 = UserType.Create(
                new UserTypeId(UserConst.UserType.User),
                UserConst.UserType.UserName,
                new CustomDateTime(DateTime.Now),
                null,
                null,
                null,
                null,
                null);

            var userType2 = UserType.Create(
                new UserTypeId(UserConst.UserType.Admin),
                UserConst.UserType.AdminName,
                new CustomDateTime(DateTime.Now),
                null,
                null,
                null,
                null,
                null);

            var userType3 = UserType.Create(
                new UserTypeId(UserConst.UserType.Root),
                UserConst.UserType.RootName,
                new CustomDateTime(DateTime.Now),
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
