using Domain.Const;
using Domain.Entities.Users;
using Domain.Primitives;
using Domain.Primitives.Ids;
using Infrastructure.EFCore.Extension;
using Infrastructure.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreMigrationTestWithInheritence_MySql_Updated.DatabaseConfiguration
{
    internal class UserTypeConfiguration : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.AddDefaultProperties<UserType, UserTypeId>();
            builder.AddAuditableProperties<UserType, UserTypeId>();

            builder.Property(ut => ut.Name)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Names)
                .HasColumnName(DbContextExtension.ColumnNameDefinitions.Name);

            builder.AddAuditableConstraints<UserType, UserTypeId>();

            var userType1 = UserType.Create(
                new UserTypeId(UserConst.UserType.User),
                "User",
                new CustomDateTime(DateTime.Now),
                null,
                null,
                null,
                null,
                null);

            var userType2 = UserType.Create(
                new UserTypeId(UserConst.UserType.Root),
                "Root",
                new CustomDateTime(DateTime.Now),
                null,
                null,
                null,
                null,
                null);

            builder.HasData(userType1, userType2);
        }
    }
}