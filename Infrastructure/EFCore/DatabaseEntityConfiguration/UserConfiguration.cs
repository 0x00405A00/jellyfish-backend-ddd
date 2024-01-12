using Domain.Const;
using Domain.Entities.Users;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.EFCore.Extension;
using Infrastructure.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infrastructure.EFCore.DatabaseEntityConfiguration
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
                .HasColumnName("date_of_birth");

            builder.Property(ut => ut.ActivationToken)
                .IsRequired(false)
                .HasMaxLength(DbContextExtension.ColumnLength.Base64)
                .HasColumnName("activation_token");

            builder.Property(ut => ut.ActivationCode)
                .IsRequired(false)
                .HasMaxLength(DbContextExtension.ColumnLength.PasswordLength)
                .HasColumnName("activation_code");

            builder.Property(ut => ut.ActivationDateTime)
                .HasColumnName("activation_datetime");

            builder.Property(ut => ut.PasswordResetCode)
                .IsRequired(false)
                .HasMaxLength(DbContextExtension.ColumnLength.PasswordLength)
                .HasColumnName("password_reset_code");

            builder.Property(ut => ut.PasswordResetToken)
                .IsRequired(false)
                .HasMaxLength(DbContextExtension.ColumnLength.PasswordLength)
                .HasColumnName("password_reset_token");

            builder.Property(ut => ut.PasswordResetExpiresIn)
                .IsRequired(false)
                .HasColumnName("password_reset_token_expires_in");

            builder.Property(ut => ut.Email)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.EmailAddrLength)
                .HasColumnName("email");

            /*
             * Data seeding currently not working with ownsone, because it determine that the Mail Property in User.cs is a navigation property...
             * 
             * builder.OwnsOne(e => e.Mail, navigationBuilder =>
            {
                    navigationBuilder.Property(e => e.EmailValue)
                        .HasColumnName("email")
                        .IsRequired();
            });*/

            builder.Property(ut => ut.Phone)
                .IsRequired(false)
                .HasMaxLength(DbContextExtension.ColumnLength.EmailAddrLength)
                .HasColumnName("phone");

            builder.OwnsOne(ut => ut.Picture, navigationBuilder =>
            {
                navigationBuilder.Property(img => img.FilePath)
                .IsRequired(false)
                .HasColumnName("profile_picture_path");
                navigationBuilder.Property(img => img.FileExtension)
                .IsRequired(false)
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


            var userToUserTypeConstraintName = DbContextExtension.GetForeignKeyName(nameof(User), nameof(User.UserTypeForeignKey), nameof(UserType));
            builder.HasOne(x => x.UserType)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.UserTypeForeignKey)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired()
                .HasConstraintName(userToUserTypeConstraintName);

            builder.AddAuditableConstraints<User, UserId>();

        }
    }
}