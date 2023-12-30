using Domain.Entities.Mails;
using Domain.Primitives.Ids;
using Infrastructure.EFCore.Extension;
using Infrastructure.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFCore.DatabaseEntityConfiguration
{
    internal class MailOutboxRecipientConfiguration : IEntityTypeConfiguration<MailOutboxRecipient>
    {
        public void Configure(EntityTypeBuilder<MailOutboxRecipient> builder)
        {
            builder.AddDefaultProperties<MailOutboxRecipient, MailOutboxRecipientId>();

            builder.Property(ut => ut.Email)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.EmailAddrLength)
                .HasColumnName("email");

            builder.Property(ut => ut.MailOutboxForeignKey)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasConversion(toDb => toDb.Id, fromDb => new MailOutboxId(fromDb))
                .HasColumnName("mail_outbox_id");

            builder.Property(ut => ut.EmailSendingTypeForeignKey)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasConversion(toDb => toDb.Id, fromDb => new EmailTypeId(fromDb))
                .HasColumnName("email_type_id");

            var mailConstraintName = DbContextExtension.GetForeignKeyName(typeof(MailOutboxRecipient).Name, nameof(MailOutboxRecipient.MailOutboxForeignKey), nameof(MailOutbox));
            builder.HasOne(u => u.Mail)
                .WithMany(x => x.Recipients)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(fk => fk.MailOutboxForeignKey)
                .HasConstraintName(mailConstraintName);

            var emailSendingTypeConstraintName = DbContextExtension.GetForeignKeyName(typeof(MailOutboxRecipient).Name, nameof(MailOutboxRecipient.EmailSendingTypeForeignKey), nameof(EmailSendingType));
            builder.HasOne(u => u.SendingType)
                .WithMany(x => x.MailOutboxRecipients)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(fk => fk.EmailSendingTypeForeignKey)
                .HasConstraintName(emailSendingTypeConstraintName);
        }
    }
}