using Infrastructure.EFCore.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Const;
using Shared.Entities.Mail;
using Shared.ValueObjects.Ids;

namespace EFCoreMigrationTestWithInheritence_MySql_Updated.DatabaseConfiguration
{
    internal class MailOutboxConfiguration : IEntityTypeConfiguration<MailOutbox>
    {
        public void Configure(EntityTypeBuilder<MailOutbox> builder)
        {
            builder.AddDefaultProperties<MailOutbox, MailOutboxId>();

            builder.Property(ut => ut.From)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.EmailAddrLength)
                .HasColumnName("from");

            builder.Property(ut => ut.Subject)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.EmailSubject)
                .HasColumnName("subject");

            builder.Property(ut => ut.Body)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Names)
                .HasColumnName("body");

            builder.Property(ut => ut.IsBodyHtml)
                .IsRequired(false)
                .HasColumnType("tinyint(1)")
                .HasDefaultValue(false)
                .HasComment("boolean value to describe if email contain html content")
                .HasColumnName("is_body_html");
        }
    }
}