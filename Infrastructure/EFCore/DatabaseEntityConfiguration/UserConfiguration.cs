using Domain.Const;
using Domain.Entities.Roles;
using Domain.Entities.Users;
using Domain.Primitives.Ids;
using Infrastructure.EFCore.Extension;
using Infrastructure.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreMigrationTestWithInheritence_MySql_Updated.DatabaseConfiguration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.AddDefaultProperties<User, UserId>();
            builder.AddAuditableProperties<User, UserId>();

            builder.Property(ut => ut.UserTypeForeignKey)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasDefaultValue(new UserTypeId(UserConst.UserType.User))
                .HasColumnName("user_type_id");

            builder.Property(ut => ut.UserName)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Names)
                .HasColumnName(DbContextExtension.ColumnNameDefinitions.Name);

            builder.Property(ut => ut.Password)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.PasswordLength)
                .HasColumnName("password");

            builder.Property(ut => ut.FirstName)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.PasswordLength)
                .HasColumnName("first_name");

            builder.Property(ut => ut.LastName)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.PasswordLength)
                .HasColumnName("last_name");

            builder.Property(ut => ut.DateOfBirth)
                .HasColumnType("datetime")
                .HasColumnName("date_of_birth");

            builder.Property(ut => ut.ActivationToken)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Base64)
                .HasColumnName("activation_token");

            builder.Property(ut => ut.ActivationCode)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.PasswordLength)
                .HasColumnName("activation_code");

            builder.Property(ut => ut.ActivationDateTime)
                .HasColumnType("datetime")
                .HasColumnName("activation_datetime");

            builder.Property(ut => ut.PasswordResetCode)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.PasswordLength)
                .HasColumnName("password_reset_code");

            builder.Property(ut => ut.PasswordResetCode)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.PasswordLength)
                .HasColumnName("password_reset_token");

            builder.Property(ut => ut.PasswordResetExpiresIn)
                .HasColumnType("datetime")
                .HasColumnName("password_reset_token_expires_in");

            builder.Property(ut => ut.Email)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.EmailAddrLength)
                .HasColumnName("email");

            builder.Property(ut => ut.Phone)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.EmailAddrLength)
                .HasColumnName("phone");

            builder.OwnsOne(ut => ut.Picture, navigationBuilder =>
            {
                navigationBuilder.Property(img => img.FilePath)
                .HasColumnName("profile_picture_path");
                navigationBuilder.Property(img => img.FileExtension)
                .HasColumnName("profile_picture_fileext");
            });



            //alte Constraints-Erstellung vor builder.AddAuditableConstraints<EUser, UserId>(); Implementierung
            /*builder.HasOne(u => u.CreatedByUser)
                .WithMany()
                .IsRequired(false)
                .HasForeignKey(fk => fk.CreatedByUserForeignKey);

            builder.HasOne(u => u.LastModifiedByUser)
                .WithMany()
                .IsRequired(false)
                .HasForeignKey(fk => fk.LastModifiedByUserForeignKey);

            builder.HasOne(u => u.DeletedByUser)
                .WithMany()
                .IsRequired(false)
                .HasForeignKey(fk => fk.DeletedByUserForeignKey);*/

            var userToUserTypeConstraintName = DbContextExtension.GetForeignKeyName(nameof(User),nameof(User.UserTypeForeignKey),nameof(UserType));
            builder.HasOne(x=>x.UserType)
                .WithMany(x=>x.Users)
                .HasForeignKey(x=>x.UserTypeForeignKey)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired()
                .HasConstraintName(userToUserTypeConstraintName);

            builder.AddAuditableConstraints<User, UserId>();

            string userHasRoleToUserConstraintName = DbContextExtension.GetForeignKeyName(nameof(UserHasRelationToRole), nameof(UserHasRelationToRole.UserForeignKey), nameof(User));
            string userHasRoleToRoleConstraintName = DbContextExtension.GetForeignKeyName(nameof(UserHasRelationToRole), nameof(UserHasRelationToRole.RoleForeignKey), nameof(Role));
            builder.HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<UserHasRelationToRole>(
                j =>
                {
                    j.HasOne(e => e.User).WithMany(e => e.UserHasRelationToRoles).HasForeignKey(e => e.UserForeignKey).HasConstraintName(userHasRoleToUserConstraintName);
                    j.HasOne(t => t.Role).WithMany(e => e.UserHasRelationToRoles).HasForeignKey(e => e.RoleForeignKey).HasConstraintName(userHasRoleToRoleConstraintName);
                });

            List<User> users = new List<User>();
            users.Add(User.GetSystemUser());
            users.AddRange(DbContextExtension.GetTestSet());
            builder.HasData(users);
        }
    }
}