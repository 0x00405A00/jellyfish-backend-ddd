using Domain.Const;
using Domain.Entities.Roles;
using Domain.Primitives;
using Domain.Primitives.Ids;
using Infrastructure.EFCore.Extension;
using Infrastructure.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreMigrationTestWithInheritence_MySql_Updated.DatabaseConfiguration
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.AddDefaultProperties<Role, RoleId>();
            builder.AddAuditableProperties<Role, RoleId>();

            builder.Property(ut => ut.Name)
                .IsRequired()
                .HasColumnName(DbContextExtension.ColumnNameDefinitions.Name);

            builder.AddAuditableConstraints<Role, RoleId>();

            var roleAdmin = Role.Create(
                new RoleId(UserConst.Role.Admin),
                "Admin",
                new CustomDateTime(DateTime.Now),
                null,
                null,
                null,
                null,
                null);

            var roleUser = Role.Create(
                new RoleId(UserConst.Role.User),
                "User",
                new CustomDateTime(DateTime.Now),
                null,
                null,
                null,
                null,
                null);
            builder.HasData(roleAdmin, roleUser);
        }
    }

}