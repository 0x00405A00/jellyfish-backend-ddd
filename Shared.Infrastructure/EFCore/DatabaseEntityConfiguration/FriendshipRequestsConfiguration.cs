using Domain.Entities.Users;
using Domain.Primitives.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Infrastructure.EFCore.Extension;
using Shared.Infrastructure.Extension;

namespace Shared.Infrastructure.EFCore.DatabaseEntityConfiguration
{
    internal class FriendshipRequestsConfiguration : IEntityTypeConfiguration<FriendshipRequest>
    {
        public void Configure(EntityTypeBuilder<FriendshipRequest> builder)
        {
            builder.AddDefaultProperties<FriendshipRequest, FriendshipRequestId>();

            var fk1Index = DbContextExtension.GetIndexForFkName(nameof(FriendshipRequest), nameof(FriendshipRequest.RequesterUser), nameof(User));
            builder.HasIndex(e => e.RequestUserForeignKey, fk1Index);

            var fk2Index = DbContextExtension.GetIndexForFkName(nameof(FriendshipRequest), nameof(FriendshipRequest.TargetUser), nameof(User));
            builder.HasIndex(e => e.TargetUserForeignKey, fk2Index);

            builder.Property(ut => ut.RequestUserForeignKey)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasColumnName("requester_id");

            builder.Property(ut => ut.TargetUserForeignKey)
                .IsRequired()
                .HasMaxLength(DbContextExtension.ColumnLength.Ids)
                .HasColumnName("target_id");

            builder.Property(ut => ut.TargetUserRequestMessage)
                .IsRequired(false)
                .HasMaxLength(DbContextExtension.ColumnLength.Descriptions)
                .HasColumnName("request_message");

            builder.HasKey(e => new { e.RequestUserForeignKey, e.TargetUserForeignKey });

            string constraintLeftName = DbContextExtension.GetForeignKeyName(nameof(FriendshipRequest), nameof(FriendshipRequest.RequestUserForeignKey), nameof(User));
            string constraintRightName = DbContextExtension.GetForeignKeyName(nameof(FriendshipRequest), nameof(FriendshipRequest.TargetUserForeignKey), nameof(User));
            builder.HasOne(e => e.RequesterUser).WithMany(x => x.FriendshipRequestsWhereIamRequester).HasForeignKey(x => x.RequestUserForeignKey).HasConstraintName(constraintLeftName).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.TargetUser).WithMany(e => e.FriendshipRequestsWhereIamTarget).HasForeignKey(e => e.TargetUserForeignKey).HasConstraintName(constraintRightName).OnDelete(DeleteBehavior.Cascade);
        }
    }
}