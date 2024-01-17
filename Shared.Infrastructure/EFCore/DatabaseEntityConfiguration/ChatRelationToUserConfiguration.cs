using Domain.Entities.Chats;
using Domain.Entities.Users;
using Domain.Primitives.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.EFCore.Extension;
using Shared.Infrastructure.Extension;

namespace Shared.Infrastructure.EFCore.DatabaseEntityConfiguration
{
    internal partial class ChatRelationToUserConfiguration : IEntityTypeConfiguration<ChatRelationToUser>
    {
        public void Configure(EntityTypeBuilder<ChatRelationToUser> builder)
        {
            builder.AddDefaultProperties<ChatRelationToUser, ChatRelationToUserId>();
            builder.AddAuditableProperties<ChatRelationToUser, ChatRelationToUserId>();

            builder.Property(ut => ut.UserForeignKey)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasColumnName("user_id");

            builder.Property(ut => ut.ChatForeignKey)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasColumnName("chat_id");

            builder.Property(ut => ut.IsChatAdmin)
                .IsRequired(false)
                .HasColumnType(DbContextExtension.ColumnType.Bool)
                .HasDefaultValue(false)
                .HasComment("boolean value to describe if chatmember is chat-admin")
                .HasColumnName("is_admin");

            builder.HasKey(e => new { e.UserForeignKey, e.ChatForeignKey });

            string chatRelationToUserConstraintName = DbContextExtension.GetForeignKeyName(nameof(ChatRelationToUser), nameof(ChatRelationToUser.UserForeignKey), nameof(User));
            string chatRelationToChatConstraintName = DbContextExtension.GetForeignKeyName(nameof(ChatRelationToUser), nameof(ChatRelationToUser.ChatForeignKey), nameof(Chat));
            builder.HasOne(e => e.User)
                .WithMany(x => x.ChatRelationToUsers)
                .HasForeignKey(x => x.UserForeignKey)
                .HasConstraintName(chatRelationToUserConstraintName)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Chat)
                .WithMany(e => e.ChatRelationToUsers)
                .HasForeignKey(e => e.ChatForeignKey)
                .HasConstraintName(chatRelationToChatConstraintName)
                .OnDelete(DeleteBehavior.Cascade);

            builder.AddAuditableConstraints<ChatRelationToUser, ChatRelationToUserId>();
        }
    }
}