using Infrastructure.EFCore.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Const;
using Shared.Entities.Chats;
using Shared.Entities.Users;
using Shared.ValueObjects.Ids;

namespace EFCoreMigrationTestWithInheritence_MySql_Updated.DatabaseConfiguration
{
    internal class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.AddDefaultProperties<Message, MessageId>();
            builder.AddAuditableProperties<Message, MessageId>();

            builder.Property(ut => ut.UserForeignKey)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasColumnName("user_id");

            builder.Property(ut => ut.ChatForeignKey)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasColumnName("chat_id");

            builder.Property(ut => ut.Text)
                .IsRequired(false)
                .HasColumnType("TEXT")
                .HasColumnName("text");
            builder.Property(ut => ut.BinaryContentPath)
                .IsRequired(false)
                .HasMaxLength(DbContextExtension.ColumnLength.PathDescriptors)
                .HasColumnName("binary_content_path");
            builder.Property(ut => ut.BinaryContentPathFileExtension)
                .IsRequired(false)
                .HasMaxLength(DbContextExtension.ColumnLength.FileExtension)
                .HasColumnName("binary_content_path_file_extension");
            builder.Property(ut => ut.BinaryContentBase64)
                .IsRequired(false)
                .HasColumnType("TEXT")
                .HasColumnName("binary_content_base64");
            builder.Property(ut => ut.BinaryContentBase64MimeType)
                .IsRequired(false)
                .HasMaxLength(DbContextExtension.ColumnLength.MimeTypes)
                .HasColumnName("binary_content_base64_mime_type");

            string messageRelationToUserConstraintName = DbContextExtension.GetForeignKeyName(nameof(Message), nameof(Message.UserForeignKey), nameof(User));
            string messageRelationToChatConstraintName = DbContextExtension.GetForeignKeyName(nameof(Message), nameof(Message.ChatForeignKey), nameof(Chat));
            builder.HasOne(e => e.User)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.UserForeignKey)
                .HasConstraintName(messageRelationToUserConstraintName)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.Chat)
                .WithMany(e => e.Messages)
                .HasForeignKey(e => e.ChatForeignKey)
                .HasConstraintName(messageRelationToChatConstraintName)
                .OnDelete(DeleteBehavior.Cascade);

            builder.AddAuditableConstraints<Message, MessageId>();
        }
    }
}