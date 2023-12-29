using Infrastructure.EFCore.Extension;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Const;
using Shared.Entities.Chats;
using Shared.Entities.Users;
using Shared.ValueObjects.Ids;

namespace EFCoreMigrationTestWithInheritence_MySql_Updated.DatabaseConfiguration
{
    internal class ChatInviteRequestConfiguration : IEntityTypeConfiguration<ChatInviteRequest>
    {
        public void Configure(EntityTypeBuilder<ChatInviteRequest> builder)
        {
            builder.AddDefaultProperties<ChatInviteRequest, ChatInviteRequestId>();

            var fk1Index = DbContextExtension.GetIndexForFkName(nameof(ChatInviteRequest), nameof(ChatInviteRequest.RequesterUser), nameof(User));
            builder.HasIndex(e => e.RequesterUserForeignKey, fk1Index);

            var fk2Index = DbContextExtension.GetIndexForFkName(nameof(ChatInviteRequest), nameof(ChatInviteRequest.TargetUser), nameof(User));
            builder.HasIndex(e => e.TargetUserForeignKey, fk2Index);

            var fk3Index = DbContextExtension.GetIndexForFkName(nameof(ChatInviteRequest), nameof(ChatInviteRequest.Chat), nameof(Chat));
            builder.HasIndex(e => e.ChatForeignKey, fk3Index);

            builder.Property(ut => ut.RequesterUserForeignKey)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasColumnName("requester_id");

            builder.Property(ut => ut.TargetUserForeignKey)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasColumnName("target_id");

            builder.Property(ut => ut.ChatForeignKey)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasColumnName("chat_id");

            builder.Property(ut => ut.TargetUserRequestMessage)
                .IsRequired(false)
                .HasMaxLength(DbContextExtension.ColumnLength.Descriptions)
                .HasColumnName("request_message");

            builder.HasKey(e => new { e.ChatForeignKey, e.TargetUserForeignKey });

            string constraintLeftName = DbContextExtension.GetForeignKeyName(nameof(ChatInviteRequest), nameof(ChatInviteRequest.RequesterUserForeignKey), nameof(User));
            string constraintRightName = DbContextExtension.GetForeignKeyName(nameof(ChatInviteRequest), nameof(ChatInviteRequest.TargetUserForeignKey), nameof(User));
            string constraintToChatName = DbContextExtension.GetForeignKeyName(nameof(ChatInviteRequest), nameof(ChatInviteRequest.ChatForeignKey), nameof(Chat));
            builder.HasOne(e => e.RequesterUser).WithMany(x => x.ChatInvitesWhereIamRequester).HasForeignKey(x => x.RequesterUserForeignKey).HasConstraintName(constraintLeftName).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.TargetUser).WithMany(e => e.ChatInvitesWhereIamTarget).HasForeignKey(e => e.TargetUserForeignKey).HasConstraintName(constraintRightName).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.Chat).WithMany(e => e.ChatInvitesToUsers).HasForeignKey(e => e.ChatForeignKey).HasConstraintName(constraintToChatName).OnDelete(DeleteBehavior.Cascade);
        }
    }
}