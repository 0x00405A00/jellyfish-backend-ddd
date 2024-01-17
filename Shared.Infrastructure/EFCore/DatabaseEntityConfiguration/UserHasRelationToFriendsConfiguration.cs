using Domain.Entities.Users;
using Domain.Primitives.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.EFCore.Extension;
using Shared.Infrastructure.Extension;

namespace Shared.Infrastructure.EFCore.DatabaseEntityConfiguration
{
    internal class UserHasRelationToFriendsConfiguration : IEntityTypeConfiguration<UserHasRelationToFriend>
    {
        public void Configure(EntityTypeBuilder<UserHasRelationToFriend> builder)
        {
            builder.AddDefaultProperties<UserHasRelationToFriend, UserHasRelationToFriendId>();
            builder.AddAuditableProperties<UserHasRelationToFriend, UserHasRelationToFriendId>();

            var fk1Index = DbContextExtension.GetIndexForFkName(nameof(UserHasRelationToFriend), nameof(UserHasRelationToFriend.UserForeignKey), nameof(User));
            builder.HasIndex(e => e.UserForeignKey, fk1Index);

            var fk2Index = DbContextExtension.GetIndexForFkName(nameof(UserHasRelationToFriend), nameof(UserHasRelationToFriend.UserFriendForeignKey), nameof(User));
            builder.HasIndex(e => e.UserFriendForeignKey, fk2Index);

            builder.Property(ut => ut.UserFriendForeignKey)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasColumnName("friend_id");

            builder.Property(ut => ut.UserForeignKey)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasColumnName("user_id");

            builder.HasKey(e => new { e.UserForeignKey, e.UserFriendForeignKey });

            string userFriendsUserToUserConstraintName = DbContextExtension.GetForeignKeyName(nameof(UserHasRelationToFriend), nameof(UserHasRelationToFriend.UserForeignKey), nameof(User));
            string userFriendsFriendToUserConstraintName = DbContextExtension.GetForeignKeyName(nameof(UserHasRelationToFriend), nameof(UserHasRelationToFriend.UserFriendForeignKey), nameof(User));
            builder.HasOne(e => e.User)
                .WithMany(x => x.FriendshipsThatIAccepted)
                .HasForeignKey(x => x.UserForeignKey)
                .HasConstraintName(userFriendsUserToUserConstraintName)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.UserFriend)
                .WithMany(e => e.FriendshipsThatIRequested)
                .HasForeignKey(e => e.UserFriendForeignKey)
                .HasConstraintName(userFriendsFriendToUserConstraintName)
                .OnDelete(DeleteBehavior.Cascade);

            builder.AddAuditableConstraints<UserHasRelationToFriend, UserHasRelationToFriendId>();
        }
    }
}