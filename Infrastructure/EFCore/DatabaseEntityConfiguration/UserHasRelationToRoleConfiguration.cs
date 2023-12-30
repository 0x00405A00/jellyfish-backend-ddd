using Domain.Const;
using Domain.Entities.Roles;
using Domain.Entities.Users;
using Domain.Primitives.Ids;
using Infrastructure.EFCore.Extension;
using Infrastructure.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFCore.DatabaseEntityConfiguration
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
                .HasDefaultValue(new RoleId(UserConst.Role.User))
                .HasColumnName("role_id");

            builder.Property(ut => ut.UserForeignKey)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasColumnName("user_id");

            builder.AddAuditableConstraints<UserHasRelationToRole, UserHasRelationToRoleId>();
        }
    }
}