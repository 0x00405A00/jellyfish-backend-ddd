using Domain.Const;
using Domain.Entities.Mails;
using Domain.Primitives;
using Domain.Primitives.Ids;
using Infrastructure.EFCore.Extension;
using Infrastructure.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFCore.DatabaseEntityConfiguration
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

            builder.HasData(toType, ccType, bccType);
        }
    }

}