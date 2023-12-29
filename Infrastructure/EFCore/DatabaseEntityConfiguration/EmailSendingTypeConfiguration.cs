using Infrastructure.EFCore.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Const;
using Shared.Entities.Mail;
using Shared.Primitives;
using Shared.ValueObjects.Ids;

namespace EFCoreMigrationTestWithInheritence_MySql_Updated.DatabaseConfiguration
{
    internal class EmailSendingTypeConfiguration : IEntityTypeConfiguration<EmailSendingType>
    {
        public void Configure(EntityTypeBuilder<EmailSendingType> builder)
        {
            builder.AddDefaultProperties<EmailSendingType, EmailTypeId>();

            builder.Property(ut => ut.Name)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Names)
                .HasColumnName(DbContextExtension.ColumnNameDefinitions.Name);

            var toType = EmailSendingType.Create(
                new EmailTypeId(Email.Type.To),
                "to",
                new CustomDateTime(DateTime.Now),
                null,
                null);

            var  ccType = EmailSendingType.Create(
                new EmailTypeId(Email.Type.Cc),
                "cc",
                new CustomDateTime(DateTime.Now),
                null,
                null);

            var bccType = EmailSendingType.Create(
                new EmailTypeId(Email.Type.Bcc),
                "bcc",
                new CustomDateTime(DateTime.Now),
                null,
                null);

            builder.HasData(toType, ccType, bccType);
        }
    }

}