using Domain.Entities.Auth;
using Domain.Entities.Chats;
using Domain.Entities.MailoutBox;
using Domain.Entities.Message;
using Domain.Entities.Role;
using Domain.Entities.User;
using Domain.Primitives;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DatabaseEntityConfiguration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.BuildTableBase<UserId, User>();

            builder.Property(ut => ut.UserTypeId)
                .IsRequired()
                .HasMaxLength(ApplicationDbContext.ColumnLength.Ids)
                .HasColumnName("user_type_uuid")
                .HasConversion(
                    toDb => toDb.Id,
                    fromDb => new UserTypeId(fromDb)
                    );

            var fkUserToUserType = ApplicationDbContext.GetForeignKeyName(nameof(User),nameof(UserType));   
            builder.HasOne(d => d.UserType)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.UserTypeId.Id)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName(fkUserToUserType);
        }
    }
    internal static class EntityTypeConfigurationExtension
    {
        public static EntityTypeBuilder BuildTableBase<TId,TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TId : Identification
            where TEntity : Entity<TId>
        {

            string tableName = ApplicationDbContext.GetTableName(typeof(TEntity));
            builder.ToTable(tableName);

            builder.HasKey(ut => ut.Id);

            var keyIndex = ApplicationDbContext.GetIndexName(nameof(Entity<TId>.Id));
            builder.HasIndex(e => e.Id, keyIndex);

            builder.Property(ut => ut.Id)
                .IsRequired()
                .HasMaxLength(ApplicationDbContext.ColumnLength.Ids)
                .HasColumnName("id")
                .HasConversion(
                    toDb => toDb.Id,
                    fromDb => (TId)new Identification(fromDb)
                    );

            return builder;
        }
    }
    internal class UserTypeConfiguration : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.BuildTableBase<UserTypeId, UserType>();

            builder.Property(ut => ut.Name)
                .IsRequired()
                .HasMaxLength(ApplicationDbContext.ColumnLength.Names); 

        }
    }
    internal class UserFriendConfiguration : IEntityTypeConfiguration<UserFriend>
    {
        public void Configure(EntityTypeBuilder<UserFriend> builder)
        {
            throw new NotImplementedException();
        }
    }
    internal class UserFriendshipRequestConfiguration : IEntityTypeConfiguration<FriendshipRequest>
    {
        public void Configure(EntityTypeBuilder<FriendshipRequest> builder)
        {
            throw new NotImplementedException();
        }
    }
    internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            throw new NotImplementedException();
        }
    }
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            throw new NotImplementedException();
        }
    }
    internal class AuthConfiguration : IEntityTypeConfiguration<Auth>
    {
        public void Configure(EntityTypeBuilder<Auth> builder)
        {
            throw new NotImplementedException();
        }
    }
    internal class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            throw new NotImplementedException();
        }
    }
    internal class ChatMemberConfiguration : IEntityTypeConfiguration<ChatMember>
    {
        public void Configure(EntityTypeBuilder<ChatMember> builder)
        {
            throw new NotImplementedException();
        }
    }
    internal class ChatMessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            throw new NotImplementedException();
        }
    }
    internal class EmailTypeConfiguration : IEntityTypeConfiguration<EmailType>
    {
        public void Configure(EntityTypeBuilder<EmailType> builder)
        {
            throw new NotImplementedException();
        }
    }
    internal class MailConfiguration : IEntityTypeConfiguration<MailOutbox>
    {
        public void Configure(EntityTypeBuilder<MailOutbox> builder)
        {
            throw new NotImplementedException();
        }
    }
    internal class MailRecipientConfiguration : IEntityTypeConfiguration<MailOutboxRecipient>
    {
        public void Configure(EntityTypeBuilder<MailOutboxRecipient> builder)
        {
            throw new NotImplementedException();
        }
    }
    internal class MailAttachmentConfiguration : IEntityTypeConfiguration<MailOutboxAttachment>
    {
        public void Configure(EntityTypeBuilder<MailOutboxAttachment> builder)
        {
            throw new NotImplementedException();
        }
    }
}
