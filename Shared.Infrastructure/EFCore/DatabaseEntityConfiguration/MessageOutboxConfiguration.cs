using Domain.Entities.Chats;
using Domain.Entities.Messages;
using Domain.Entities.Users;
using Domain.Primitives.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.EFCore.Extension;
using Shared.Infrastructure.Extension;

namespace Shared.Infrastructure.EFCore.DatabaseEntityConfiguration
{
    internal class MessageOutboxConfiguration : IEntityTypeConfiguration<MessageOutbox>
    {
        public void Configure(EntityTypeBuilder<MessageOutbox> builder)
        {
            builder.AddDefaultProperties<MessageOutbox, MessageOutboxId>();

            builder.Property(ut => ut.UserForeignKey)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasColumnName("user_id");

            builder.Property(ut => ut.MessageForeignKey)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasColumnName("message_id");

            string messageOutboxRelationToUserConstraintName = DbContextExtension.GetForeignKeyName(nameof(MessageOutbox), nameof(MessageOutbox.UserForeignKey), nameof(User));
            string messageOutboxRelationToChatConstraintName = DbContextExtension.GetForeignKeyName(nameof(MessageOutbox), nameof(MessageOutbox.MessageForeignKey), nameof(Chat));
            builder.HasOne(e => e.User)
                .WithMany(x => x.MessagesInOutbox)
                .HasForeignKey(x => x.UserForeignKey)
                .HasConstraintName(messageOutboxRelationToUserConstraintName)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Message)
                .WithMany(e => e.MessagesInOutbox)
                .HasForeignKey(e => e.MessageForeignKey)
                .HasConstraintName(messageOutboxRelationToChatConstraintName)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(e => new { e.MessageForeignKey, e.UserForeignKey });
        }
    }
}