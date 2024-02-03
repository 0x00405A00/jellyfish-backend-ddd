using Domain.Const;
using Domain.Entities.Mails;
using Domain.Entities.Users;
using Domain.Primitives;
using Domain.Primitives.Ids;
using Microsoft.EntityFrameworkCore;

namespace Shared.Infrastructure.EFCore
{
    public static class DataSeedExtension
    {
        public static ModelBuilder CreateEmailSendingTypes(this ModelBuilder modelBuilder)
        {
            var toType = EmailSendingType.Create(
                new EmailTypeId(EmailConst.Type.To),
                EmailConst.Type.ToName,
                CustomDateTime.Now(),
                null,
                null);

            var ccType = EmailSendingType.Create(
                new EmailTypeId(EmailConst.Type.Cc),
                EmailConst.Type.CcName,
                CustomDateTime.Now(),
                null,
                null);

            var bccType = EmailSendingType.Create(
                new EmailTypeId(EmailConst.Type.Bcc),
                EmailConst.Type.BccName,
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
