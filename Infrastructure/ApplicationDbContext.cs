using Infrastructure.DatabaseEntity;
using Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure;

internal partial class ApplicationDbContext : DbContext
{
    public IConfiguration _configuration { get; }
    public ApplicationDbContext(
            IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public ApplicationDbContext(
            IConfiguration configuration, DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        _configuration = configuration;
    }
    public DbContextAuditLogInterceptor DbContextAuditLogInterceptor { get; }
    public ApplicationDbContext(
            IConfiguration configuration, DbContextAuditLogInterceptor dbContextAuditLogInterceptor)
    {
        _configuration = configuration;
        DbContextAuditLogInterceptor = dbContextAuditLogInterceptor;
    }

    public ApplicationDbContext(
            IConfiguration configuration, DbContextOptions<ApplicationDbContext> options, DbContextAuditLogInterceptor dbContextAuditLogInterceptor)
        : base(options)
    {
        _configuration = configuration;
        DbContextAuditLogInterceptor = dbContextAuditLogInterceptor;
    }


    public virtual DbSet<Auth> Auths { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<ChatRelationToUser> ChatRelationToUsers { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<MessageAcknowledge> MessageAcknowledges { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserActiveView> UserActiveViews { get; set; }

    public virtual DbSet<UserChatInviteRequest> UserChatInviteRequests { get; set; }

    public virtual DbSet<UserFriend> UserFriends { get; set; }

    public virtual DbSet<UserFriendshipRequest> UserFriendshipRequests { get; set; }

    public virtual DbSet<UserRelationToRole> UserRelationToRoles { get; set; }

    public virtual DbSet<UserRoleView> UserRoleViews { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    public virtual DbSet<EmailType> EmailTypes { get; set; }

    public virtual DbSet<MailOutbox> MailOutboxes { get; set; }

    public virtual DbSet<MailOutboxAttachment> MailOutboxAttachments { get; set; }

    public virtual DbSet<MailOutboxRecipient> MailOutboxRecipients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    {
        var connectionString = _configuration.GetConnectionString("JellyfishMySqlDatabase");
        optionsBuilder.UseMySQL(connectionString);
        optionsBuilder.AddInterceptors(DbContextAuditLogInterceptor);
        optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        optionsBuilder.ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
        optionsBuilder.EnableSensitiveDataLogging ( true);

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auth>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.ToTable("auth");

            entity.HasIndex(e => e.UserUuid, "fk_authToUser_idx");

            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasColumnName("uuid");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_time");
            entity.Property(e => e.DeletedTime)
                .HasColumnType("datetime")
                .HasColumnName("deleted_time");
            entity.Property(e => e.IpAddrv4Local)
                .HasMaxLength(45)
                .HasColumnName("ip_addrv4_local");
            entity.Property(e => e.IpAddrv4Remote)
                .HasMaxLength(45)
                .HasColumnName("ip_addrv4_remote");
            entity.Property(e => e.IpAddrv6Local)
                .HasMaxLength(150)
                .HasColumnName("ip_addrv6_local");
            entity.Property(e => e.IpAddrv6Remote)
                .HasMaxLength(150)
                .HasColumnName("ip_addrv6_remote");
            entity.Property(e => e.LastModifiedTime)
                .HasColumnType("datetime")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.LocalPort).HasColumnName("local_port");
            entity.Property(e => e.LogoutDatetime)
                .HasColumnType("datetime")
                .HasColumnName("logout_datetime");
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(2000)
                .HasColumnName("refresh_token");
            entity.Property(e => e.RefreshTokenExpiresIn)
                .HasColumnType("datetime")
                .HasColumnName("refresh_token_expires_in");
            entity.Property(e => e.RemotePort).HasColumnName("remote_port");
            entity.Property(e => e.Token)
                .HasMaxLength(2000)
                .HasColumnName("token");
            entity.Property(e => e.TokenExpiresIn)
                .HasColumnType("datetime")
                .HasColumnName("token_expires_in");
            entity.Property(e => e.UserAgent)
                .HasColumnType("text")
                .HasColumnName("user_agent");
            entity.Property(e => e.UserUuid)
                .HasMaxLength(36)
                .HasColumnName("user_uuid");

            entity.HasOne(d => d.UserUu).WithMany(p => p.Auths)
                .HasForeignKey(d => d.UserUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_authToUser");
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.ToTable("chat");

            entity.HasIndex(e => e.OwnerUserUuid, "fk_chatToUser_idx");

            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasColumnName("uuid");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_time");
            entity.Property(e => e.DeletedTime)
                .HasColumnType("datetime")
                .HasColumnName("deleted_time");
            entity.Property(e => e.LastModifiedTime)
                .HasColumnType("datetime")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.Description)
                .HasMaxLength(2048)
                .HasColumnName("description");
            entity.Property(e => e.OwnerUserUuid)
                .HasMaxLength(36)
                .HasColumnName("owner_user_uuid");
            entity.Property(e => e.Picture)
                .HasComment("Base64 von Pic Binary")
                .HasColumnType("blob")
                .HasColumnName("picture");

            entity.HasOne(d => d.OwnerUserUu).WithMany(p => p.Chats)
                .HasForeignKey(d => d.OwnerUserUuid)
                .HasConstraintName("fk_chatToUser");
        });

        modelBuilder.Entity<ChatRelationToUser>(entity =>
        {
            entity.HasKey(e => new { e.ChatUuid,e.UserUuid}).HasName("PRIMARY");

            entity.ToTable("chat_relation_to_user");

            entity.HasIndex(e => e.ChatUuid, "fkChatToChat_idx");

            entity.HasIndex(e => e.UserUuid, "fkChatToUser_idx");

            entity.Property(e => e.ChatUuid)
                .HasMaxLength(36)
                .HasColumnName("chat_uuid");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_time");
            entity.Property(e => e.IsChatAdmin)
                .HasDefaultValueSql("'0'")
                .HasColumnName("is_chat_admin");
            entity.Property(e => e.DeletedTime)
                .HasColumnType("datetime")
                .HasColumnName("deleted_time");
            entity.Property(e => e.LastModifiedTime)
                .HasColumnType("datetime")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.UserUuid)
                .HasMaxLength(36)
                .HasColumnName("user_uuid");

            entity.HasOne(d => d.ChatUu).WithMany(p => p.ChatRelationToUsers)
                .HasForeignKey(d => d.ChatUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkChatToChat");

            entity.HasOne(d => d.UserUu).WithMany(p => p.ChatRelationToUsers)
                .HasForeignKey(d => d.UserUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkChatToUser");
        });

        modelBuilder.Entity<EmailType>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.ToTable("email_type");

            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasColumnName("uuid");
            entity.Property(e => e.Type)
                .HasMaxLength(45)
                .HasColumnName("type");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_time");
            entity.Property(e => e.DeletedTime)
                .HasColumnType("datetime")
                .HasColumnName("deleted_time");
            entity.Property(e => e.LastModifiedTime)
                .HasColumnType("datetime")
                .HasColumnName("last_modified_time");
        });

        modelBuilder.Entity<MailOutbox>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");
            entity.ToTable("mail_outbox");

            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasColumnName("uuid");
            entity.Property(e => e.Body).HasColumnName("body");
            entity.Property(e => e.BodyIsHtml)
                .HasDefaultValueSql("'0'")
                .HasColumnName("body_is_html");
            entity.Property(e => e.From)
                .HasMaxLength(255)
                .HasColumnName("from");
            entity.Property(e => e.Subject)
                .HasMaxLength(255)
                .UseCollation("utf8mb4_unicode_ci")
                .HasColumnName("subject");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_time");
            entity.Property(e => e.DeletedTime)
                .HasColumnType("datetime")
                .HasColumnName("deleted_time");
            entity.Property(e => e.LastModifiedTime)
                .HasColumnType("datetime")
                .HasColumnName("last_modified_time");
        });

        modelBuilder.Entity<MailOutboxAttachment>(entity =>
        {
            entity.HasKey(e => new { e.MailUuid, e.Uuid }).HasName("PRIMARY");

            entity.ToTable("mail_outbox_attachment");

            entity.Property(e => e.MailUuid)
                .HasMaxLength(36)
                .HasColumnName("mail_uuid");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasColumnName("uuid");
            entity.Property(e => e.AttachmentPath).HasColumnName("attachment_path");
            entity.Property(e => e.AttachmentSha1)
                .HasMaxLength(45)
                .HasColumnName("attachment_sha1");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_time");
            entity.Property(e => e.DeletedTime)
                .HasColumnType("datetime")
                .HasColumnName("deleted_time");
            entity.Property(e => e.Filename)
                .HasMaxLength(255)
                .HasColumnName("filename");
            entity.Property(e => e.IsEmbeddedInHtml)
                .HasDefaultValueSql("'0'")
                .HasColumnName("is_embedded_in_html");
            entity.Property(e => e.LastModifiedTime)
                .HasColumnType("datetime")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.MimeCid)
                .HasMaxLength(45)
                .HasColumnName("mime_cid");
            entity.Property(e => e.MimeMediasubtype)
                .HasMaxLength(45)
                .HasColumnName("mime_mediasubtype");
            entity.Property(e => e.MimeMediatype)
                .HasMaxLength(45)
                .HasColumnName("mime_mediatype");
            entity.Property(e => e.Order).HasColumnName("order");

            entity.HasOne(d => d.MailUu).WithMany(p => p.MailOutboxAttachments)
                .HasForeignKey(d => d.MailUuid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_mailOutboxAttachmentToMailOutbox");
        });

        modelBuilder.Entity<MailOutboxRecipient>(entity =>
        {
            entity.HasKey(e => new { e.MailUuid, e.Email, e.EmailTypeUuid }).HasName("PRIMARY");

            entity.ToTable("mail_outbox_recipient");

            entity.HasIndex(e => e.EmailTypeUuid, "fk_mailOutboxRecipientToEmailType_idx");

            entity.Property(e => e.MailUuid)
                .HasMaxLength(36)
                .HasColumnName("mail_uuid");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.EmailTypeUuid)
                .HasMaxLength(36)
                .HasColumnName("email_type_uuid");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_time");
            entity.Property(e => e.DeletedTime)
                .HasColumnType("datetime")
                .HasColumnName("deleted_time");
            entity.Property(e => e.LastModifiedTime)
                .HasColumnType("datetime")
                .HasColumnName("last_modified_time");

            entity.HasOne(d => d.EmailTypeUu).WithMany(p => p.MailOutboxRecipients)
                .HasForeignKey(d => d.EmailTypeUuid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_mailOutboxRecipientToEmailType");

            entity.HasOne(d => d.MailUu).WithMany(p => p.MailOutboxRecipients)
                .HasForeignKey(d => d.MailUuid)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_mailOutboxRecipientToMailOutbox");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.ToTable("message");

            entity.HasIndex(e => e.MessageOwner, "fkMessageOwnerToUser_idx");

            entity.HasIndex(e => e.ChatUuid, "fkMessageToChat_idx");

            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasColumnName("uuid");
            entity.Property(e => e.BinaryContent)
                .HasColumnType("blob")
                .HasColumnName("binary_content");
            entity.Property(e => e.ChatUuid)
                .HasMaxLength(36)
                .HasColumnName("chat_uuid");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_time");
            entity.Property(e => e.DeletedTime)
                .HasColumnType("datetime")
                .HasColumnName("deleted_time");
            entity.Property(e => e.LastModifiedTime)
                .HasColumnType("datetime")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.MessageOwner)
                .HasMaxLength(36)
                .HasColumnName("message_owner");
            entity.Property(e => e.Text)
                .HasColumnType("text")
                .HasColumnName("text");

            entity.HasOne(d => d.ChatUu).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ChatUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkMessageToChat");

            entity.HasOne(d => d.MessageOwnerNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.MessageOwner)
                .HasConstraintName("fkMessageOwnerToUser");
        });

        modelBuilder.Entity<MessageAcknowledge>(entity =>
        {
            entity.HasKey(e => e.MessageUuid).HasName("PRIMARY");

            entity.ToTable("message_acknowledge");

            entity.HasIndex(e => e.UserUuid, "fkMessageAckToUser_idx");

            entity.Property(e => e.MessageUuid)
                .HasMaxLength(36)
                .HasColumnName("message_uuid");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_time");
            entity.Property(e => e.DeletedTime)
                .HasColumnType("datetime")
                .HasColumnName("deleted_time");
            entity.Property(e => e.LastModifiedTime)
                .HasColumnType("datetime")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.UserUuid)
                .HasMaxLength(36)
                .HasColumnName("user_uuid");

            entity.HasOne(d => d.MessageUu).WithOne(p => p.MessageAcknowledge)
                .HasForeignKey<MessageAcknowledge>(d => d.MessageUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkMessageAckToMessage");

            entity.HasOne(d => d.UserUu).WithMany(p => p.MessageAcknowledges)
                .HasForeignKey(d => d.UserUuid)
                .HasConstraintName("fkMessageAckToUser");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.ToTable("role");

            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasColumnName("uuid");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_time");
            entity.Property(e => e.DeletedTime)
                .HasColumnType("datetime")
                .HasColumnName("deleted_time");
            entity.Property(e => e.Description)
                .HasMaxLength(128)
                .HasColumnName("description");
            entity.Property(e => e.LastModifiedTime)
                .HasColumnType("datetime")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.UserTypeUuid, "fk_userToUserType_idx");

            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasColumnName("uuid");
            entity.Property(e => e.ActivationCode)
                .HasMaxLength(4)
                .HasColumnName("activation_code");
            entity.Property(e => e.ActivationDatetime)
                .HasColumnType("datetime")
                .HasColumnName("activation_datetime");
            entity.Property(e => e.ActivationToken)
                .HasMaxLength(1024)
                .HasColumnName("activation_token");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_time");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.DeletedTime)
                .HasColumnType("datetime")
                .HasColumnName("deleted_time");
            entity.Property(e => e.FirstName)
                .HasMaxLength(45)
                .HasColumnName("first_name");
            entity.Property(e => e.LastModifiedTime)
                .HasColumnType("datetime")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.LastName)
                .HasMaxLength(45)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(45)
                .HasColumnName("password");
            entity.Property(e => e.PasswordResetCode)
                .HasMaxLength(10)
                .HasColumnName("password_reset_code");
            entity.Property(e => e.PasswordResetExpiresIn)
                .HasColumnType("datetime")
                .HasColumnName("password_reset_expires_in");
            entity.Property(e => e.PasswordResetToken)
                .HasMaxLength(1024)
                .HasColumnName("password_reset_token");
            entity.Property(e => e.Phone)
                .HasMaxLength(45)
                .HasColumnName("phone");
            entity.Property(e => e.Picture)
                .HasComment("Base64 von Pic Binary")
                .HasColumnName("picture");
            entity.Property(e => e.UserName)
                .HasMaxLength(64)
                .HasColumnName("user_name");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.UserTypeUuid)
                .HasMaxLength(36)
                .HasColumnName("user_type_uuid");

            entity.HasOne(d => d.UserTypeUu).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserTypeUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_userToUserType");
        });

        modelBuilder.Entity<UserActiveView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("user_active_view");
        });

        modelBuilder.Entity<UserChatInviteRequest>(entity =>
        {
            entity.HasKey(e => new { e.ChatUuid, e.TargetUserUuid }).HasName("PRIMARY");

            entity.ToTable("user_chat_invite_request");

            entity.HasIndex(e => e.TargetUserUuid, "fkUserChatInviteRequestToUser2_idx");

            entity.HasIndex(e => e.UserUuid, "fkUserChatInviteRequestToUser_idx");

            entity.Property(e => e.ChatUuid)
                .HasMaxLength(36)
                .HasColumnName("chat_uuid");
            entity.Property(e => e.TargetUserUuid)
                .HasMaxLength(36)
                .HasColumnName("target_user_uuid");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_time");
            entity.Property(e => e.DeletedTime)
                .HasColumnType("datetime")
                .HasColumnName("deleted_time");
            entity.Property(e => e.LastModifiedTime)
                .HasColumnType("datetime")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.TargetUserRequestMessage)
                .HasMaxLength(1024)
                .HasColumnName("target_user_request_message");
            entity.Property(e => e.UserUuid)
                .HasMaxLength(36)
                .HasColumnName("user_uuid");

            entity.HasOne(d => d.ChatUu).WithMany(p => p.UserChatInviteRequests)
                .HasForeignKey(d => d.ChatUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkUserChatInviteRequestToChat");

            entity.HasOne(d => d.TargetUserUu).WithMany(p => p.UserChatInviteRequestTargetUserUus)
                .HasForeignKey(d => d.TargetUserUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkUserChatInviteRequestToUser2");

            entity.HasOne(d => d.UserUu).WithMany(p => p.UserChatInviteRequestUserUus)
                .HasForeignKey(d => d.UserUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkUserChatInviteRequestToUser1");
        });


        modelBuilder.Entity<UserFriend>(entity =>
        {
            entity.HasKey(e => new { e.UserUuid, e.FriendUserUuid }).HasName("PRIMARY");

            entity.ToTable("user_friends");

            entity.HasIndex(e => e.FriendUserUuid, "fk_UserFriendFriendToUser_idx");

            entity.Property(e => e.UserUuid)
                .HasMaxLength(36)
                .HasColumnName("user_uuid");
            entity.Property(e => e.FriendUserUuid)
                .HasMaxLength(36)
                .HasColumnName("friend_user_uuid");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_time");
            entity.Property(e => e.DeletedTime)
                .HasColumnType("datetime")
                .HasColumnName("deleted_time");
            entity.Property(e => e.LastModifiedTime)
                .HasColumnType("datetime")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasColumnName("uuid");

            entity.HasOne(d => d.FriendUserUu).WithMany(p => p.UserFriendFriendUserUus)
                .HasForeignKey(d => d.FriendUserUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_UserFriendFriendToUser");

            entity.HasOne(d => d.UserUu).WithMany(p => p.UserFriendUserUus)
                .HasForeignKey(d => d.UserUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_UserFriendUserToUser");
        });

        modelBuilder.Entity<UserFriendshipRequest>(entity =>
        {
            entity.HasKey(e => new { e.UserUuid, e.TargetUserUuid }).HasName("PRIMARY");

            entity.ToTable("user_friendship_request");

            entity.HasIndex(e => e.TargetUserUuid, "fkUserFriendshipRequestToUser2_idx");

            entity.Property(e => e.UserUuid)
                .HasMaxLength(36)
                .HasColumnName("user_uuid");
            entity.Property(e => e.TargetUserUuid)
                .HasMaxLength(36)
                .HasColumnName("target_user_uuid");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_time");
            entity.Property(e => e.DeletedTime)
                .HasColumnType("datetime")
                .HasColumnName("deleted_time");
            entity.Property(e => e.LastModifiedTime)
                .HasColumnType("datetime")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.TargetUserRequestMessage)
                .HasMaxLength(1024)
                .HasColumnName("target_user_request_message");

            entity.HasOne(d => d.TargetUserUu).WithMany(p => p.UserFriendshipRequestTargetUserUus)
                .HasForeignKey(d => d.TargetUserUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkUserFriendshipRequestToUser2");

            entity.HasOne(d => d.UserUu).WithMany(p => p.UserFriendshipRequestUserUus)
                .HasForeignKey(d => d.UserUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkUserFriendshipRequestToUser1");
        });

        modelBuilder.Entity<UserRelationToRole>(entity =>
        {
            entity.HasKey(e => new { e.UserUuid, e.RoleUuid }).HasName("PRIMARY");

            entity.ToTable("user_relation_to_role");

            entity.HasIndex(e => e.RoleUuid, "fk_userRoleToRole_idx");

            entity.HasIndex(e => e.UserUuid, "fk_userRoleToUser_idx");

            entity.Property(e => e.UserUuid)
                .HasMaxLength(36)
                .HasColumnName("user_uuid");
            entity.Property(e => e.RoleUuid)
                .HasMaxLength(36)
                .HasColumnName("role_uuid");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_time");
            entity.Property(e => e.DeletedTime)
                .HasColumnType("datetime")
                .HasColumnName("deleted_time");
            entity.Property(e => e.LastModifiedTime)
                .HasColumnType("datetime")
                .HasColumnName("last_modified_time");

            entity.HasOne(d => d.RoleUu).WithMany(p => p.UserRelationToRoles)
                .HasForeignKey(d => d.RoleUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_userRoleToRole");

            entity.HasOne(d => d.UserUu).WithMany(p => p.UserRelationToRoles)
                .HasForeignKey(d => d.UserUuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_userRoleToUser");
        });

        modelBuilder.Entity<UserRoleView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("user_role_view");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.HasKey(e => e.Uuid).HasName("PRIMARY");

            entity.ToTable("user_type");

            entity.Property(e => e.Uuid)
                .HasMaxLength(36)
                .HasDefaultValueSql("'UUID'")
                .HasColumnName("uuid");
            entity.Property(e => e.CreatedTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_time");
            entity.Property(e => e.DeletedTime)
                .HasColumnType("datetime")
                .HasColumnName("deleted_time");
            entity.Property(e => e.LastModifiedTime)
                .HasColumnType("datetime")
                .HasColumnName("last_modified_time");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
internal partial class ApplicationDbContextMailService : ApplicationDbContext
{
    public ApplicationDbContextMailService(IConfiguration configuration) : base(configuration)
    {
    }

    public ApplicationDbContextMailService(IConfiguration configuration, DbContextOptions<ApplicationDbContext> options) : base(configuration, options)
    {
    }

    public ApplicationDbContextMailService(IConfiguration configuration, DbContextAuditLogInterceptor dbContextAuditLogInterceptor) : base(configuration, dbContextAuditLogInterceptor)
    {
    }

    public ApplicationDbContextMailService(IConfiguration configuration, DbContextOptions<ApplicationDbContext> options, DbContextAuditLogInterceptor dbContextAuditLogInterceptor) : base(configuration, options, dbContextAuditLogInterceptor)
    {
    }
}
