using Domain.Entities.Mails;
using Domain.Primitives.Ids;
using Infrastructure.EFCore.Extension;
using Infrastructure.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFCore.DatabaseEntityConfiguration
{
    internal class MailOutboxAttachmentConfiguration : IEntityTypeConfiguration<MailOutboxAttachment>
    {
        public void Configure(EntityTypeBuilder<MailOutboxAttachment> builder)
        {
            builder.AddDefaultProperties<MailOutboxAttachment, MailOutboxAttachmentId>();

            builder.Property(ut => ut.MailOutboxForeignKey)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasConversion(toDb => toDb.Id, fromDb => new MailOutboxId(fromDb))
                .HasColumnName("mail_outbox_id");

            builder.Property(ut => ut.Filename)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.EmailSubject)
                .HasColumnName("filename");

            builder.Property(ut => ut.AttachmentPath)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Names)
                .HasColumnName("attachment_path");

            builder.Property(ut => ut.Order)
                .IsRequired()
                .HasColumnName("order");

            builder.Property(ut => ut.AttachmentSha1)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Base64)
                .HasColumnName("attachment_sha1");

            builder.Property(ut => ut.MimeMediaType)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Names)
                .HasColumnName("mime_media_type");

            builder.Property(ut => ut.MimeMediaSubType)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Names)
                .HasColumnName("mime_media_subtype");

            builder.Property(ut => ut.MimeCid)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Names)
                .HasColumnName("mime_cid");

            builder.Property(ut => ut.IsEmbededInHtml)
                .IsRequired(false)
                .HasColumnType(DbContextExtension.ColumnType.Bool)
                .HasDefaultValue(false)
                .HasComment("boolean value to describe if attachment is part of html mail")
            .HasColumnName("is_embeded_in_html");

            var mailConstraintName = DbContextExtension.GetForeignKeyName(typeof(MailOutboxAttachment).Name, nameof(MailOutboxAttachment.MailOutboxForeignKey), nameof(MailOutbox));
            builder.HasOne(u => u.Mail)
                .WithMany(x => x.Attachments)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(fk => fk.MailOutboxForeignKey)
                .HasConstraintName(mailConstraintName);
        }
    }
}