using Infrastructure.EFCore.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Const;
using Shared.Entities.Roles;
using Shared.Entities.Users;
using Shared.ValueObjects.Ids;
using System.Security.Cryptography.Xml;

namespace EFCoreMigrationTestWithInheritence_MySql_Updated.DatabaseConfiguration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.AddDefaultProperties<User, UserId>();
            builder.AddAuditableProperties<User, UserId>();

            builder.Property(ut => ut.Name)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Names)
                .HasColumnName(DbContextExtension.ColumnNameDefinitions.Name);
            
            builder.Property(ut => ut.Email)
                .IsRequired()
                .HasMaxLength (DbContextExtension.ColumnLength.EmailAddrLength)
                .HasColumnName("email");

            builder.Property(ut => ut.UserTypeForeignKey)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasDefaultValue(new UserTypeId(Shared.Const.UserConst.UserType.User))
                .HasConversion(toDb => toDb.Id, fromDb => new UserTypeId(fromDb))
                .HasColumnName("user_type_id");

            builder.Property(ut => ut.Password)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.PasswordLength)
                .HasColumnName("password");

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
            users.Add(DbContextExtension.GetRootUser());
            users.AddRange(DbContextExtension.GetTestSet());
            builder.HasData(users);
        }
    }
}