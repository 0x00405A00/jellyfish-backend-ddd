using Domain.Const;
using Domain.Entities.Roles;
using Domain.Entities.Users;
using Domain.Primitives.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.EFCore.Extension;
using Shared.Infrastructure.Extension;

namespace Shared.Infrastructure.EFCore.DatabaseEntityConfiguration
{
    internal class UserHasRelationToRoleConfiguration : IEntityTypeConfiguration<UserHasRelationToRole>
    {
        public void Configure(EntityTypeBuilder<UserHasRelationToRole> builder)
        {
            builder.AddDefaultProperties<UserHasRelationToRole, UserHasRelationToRoleId>();
            builder.AddAuditableProperties<UserHasRelationToRole, UserHasRelationToRoleId>();

            var fk1Index = DbContextExtension.GetIndexForFkName(nameof(UserHasRelationToRole), nameof(UserHasRelationToRole.UserForeignKey), nameof(User));
            builder.HasIndex(e => e.UserForeignKey, fk1Index);

            var fk2Index = DbContextExtension.GetIndexForFkName(nameof(UserHasRelationToRole), nameof(UserHasRelationToRole.RoleForeignKey), nameof(Role));
            builder.HasIndex(e => e.RoleForeignKey, fk2Index);

            builder.Property(ut => ut.RoleForeignKey)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasDefaultValue(new RoleId(RoleConst.UserRoleUuid))
                .HasColumnName("role_id");

            builder.Property(ut => ut.UserForeignKey)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasColumnName("user_id");

            string userHasRoleToUserConstraintName = DbContextExtension.GetForeignKeyName(nameof(UserHasRelationToRole), nameof(UserHasRelationToRole.UserForeignKey), nameof(User));
            string userHasRoleToRoleConstraintName = DbContextExtension.GetForeignKeyName(nameof(UserHasRelationToRole), nameof(UserHasRelationToRole.RoleForeignKey), nameof(Role));
            builder.HasOne(e => e.User)
                .WithMany(e => e.UserHasRelationToRoles)
                .HasForeignKey(e => e.UserForeignKey)
                .HasConstraintName(userHasRoleToUserConstraintName)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Role)
                .WithMany(e => e.UserHasRelationToRoles)
                .HasForeignKey(e => e.RoleForeignKey)
                .HasConstraintName(userHasRoleToRoleConstraintName)
                .OnDelete(DeleteBehavior.Cascade);

            builder.AddAuditableConstraints<UserHasRelationToRole, UserHasRelationToRoleId>();
        }
    }
}