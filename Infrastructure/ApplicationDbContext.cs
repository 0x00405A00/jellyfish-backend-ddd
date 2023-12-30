using Domain.Entities.Auths;
using Domain.Entities.Chats;
using Domain.Entities.Mails;
using Domain.Entities.Messages;
using Domain.Entities.Roles;
using Domain.Entities.Users;
using Domain.Primitives;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using EFCoreMigrationTestWithInheritence_MySql_Updated.Converter;
using EFCoreMigrationTestWithInheritence_MySql_Updated.DatabaseConfiguration;
using Infrastructure.EFCore.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public class ApplicationDbContext: DbContext
    {
        #region Const
        public const string ConnectionStringAlias = "JellyfishMySqlDatabase";
        #endregion
        #region Consumed DI Services
        public DbSaveChangesInterceptor DbContextAuditLogInterceptor { get; }
        public IConfiguration _configuration { get; }
        public DbContextOptions<ApplicationDbContext> Options { get; }
        #endregion
        #region DbSets
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserHasRelationToRole> UserHasRelationToRoles { get; set; }
        public DbSet<FriendshipRequest> FriendshipRequests { get; set; }
        public DbSet<UserHasRelationToFriend> UserFriends { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Auth> Auths { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatRelationToUser> ChatRelationToUsers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageOutbox> MessageOutboxes { get; set; }
        public DbSet<EmailSendingType> EmailTypes { get; set; }
        public DbSet<MailOutbox> MailOutboxes { get; set; }
        public DbSet<MailOutboxRecipient> MailOutboxRecipients { get; set; }
        public DbSet<MailOutboxAttachment> MailOutboxAttachments { get; set; }
        #endregion
        #region Ctor
        public ApplicationDbContext()
        {
            
        }
        public ApplicationDbContext(
                IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ApplicationDbContext(
                IConfiguration configuration, DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Options = options;
            _configuration = configuration;
        }
        public ApplicationDbContext(
                IConfiguration configuration, DbSaveChangesInterceptor dbContextAuditLogInterceptor)
        {
            _configuration = configuration;
            DbContextAuditLogInterceptor = dbContextAuditLogInterceptor;
        }

        public ApplicationDbContext(
                IConfiguration configuration, DbContextOptions<ApplicationDbContext> options, DbSaveChangesInterceptor dbContextAuditLogInterceptor)
            : base(options)
        {
            _configuration = configuration;
            Options = options;
            DbContextAuditLogInterceptor = dbContextAuditLogInterceptor;
        }
        #endregion
        #region Model
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        {
            string connectionString = @"server=127.0.0.1;port=33306;uid=jellyfish;pwd=meinDatabasePassword!;database=jellyfish;charset=utf8mb4;";//hardcoded connection string, because cli tool dotnet ef migrations/database cant consume IConfiguration-Service from DI

            optionsBuilder.UseMySQL(connectionString);
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
            optionsBuilder.ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
            optionsBuilder.EnableSensitiveDataLogging(true);
            optionsBuilder.AddInterceptors(new DatabaseReaderInterceptor());
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

#warning Before any start, check if u changed the entity structure in IEntityConfiguration classes. When any changes are done, please migrate to database 
#warning Documentation: https://learn.microsoft.com/de-de/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //pay attention to dependency order (chicken egg problem)

            //Initial Migration: PS C:\Users\Mika\source\repos\jellyfish-backend-ddd\Presentation> dotnet ef migrations add InitialCreate --context ApplicationDbContext
            //Current Problem: A key cannot be configured on 'Entity<Identification>' because it is a derived type.The key must be configured on the root type 'Entity'.If you did not intend for 'Entity' to be included in the model, ensure that it is not referenced by a DbSet property on your context, referenced in a configuration call to ModelBuilder, or referenced from a navigation on a type that is included in the model.
            modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserHasRelationToFriendsConfiguration());
            modelBuilder.ApplyConfiguration(new FriendshipRequestsConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserHasRelationToRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AuthConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
            modelBuilder.ApplyConfiguration(new ChatRelationToUserConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new MessageOutboxConfiguration());
            modelBuilder.ApplyConfiguration(new ChatInviteRequestConfiguration());
            modelBuilder.ApplyConfiguration(new EmailSendingTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MailOutboxConfiguration());
            modelBuilder.ApplyConfiguration(new MailOutboxRecipientConfiguration());
            modelBuilder.ApplyConfiguration(new MailOutboxAttachmentConfiguration());

            //modelBuilder.ApplyConfigurationsFromAssembly ignore any order so dependencies which are order depend could not be created (app runs in exception)

            //Data seeding: Schema initial data with: https://learn.microsoft.com/de-de/ef/core/modeling/data-seeding

            base.OnModelCreating(modelBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<CustomDateTime>()
                .HaveConversion<CustomDateTimeConverter>();

            configurationBuilder
                .Properties<CustomDateOnly>()
                .HaveConversion<CustomDateOnlyConverter>();

            configurationBuilder
                .Properties<UserId>()
                .HaveConversion<UserIdConverter>();

            configurationBuilder
                .Properties<UserTypeId>()
                .HaveConversion<UserTypeIdConverter>();

            configurationBuilder
                .Properties<ChatId>()
                .HaveConversion<ChatIdConverter>();

            configurationBuilder
                .Properties<MessageId>()
                .HaveConversion<MessageIdConverter>();

            configurationBuilder
                .Properties<RoleId>()
                .HaveConversion<RoleIdConverter>();

            configurationBuilder
                .Properties<PhoneNumber>()
                .HaveConversion<PhoneNumber>();

            configurationBuilder
                .Properties<Email>()
                .HaveConversion<EmailConverter>();
        }
        #endregion
    }
    internal partial class ApplicationDbContextMailService : ApplicationDbContext
    {
        public ApplicationDbContextMailService(IConfiguration configuration) : base(configuration)
        {
        }

        public ApplicationDbContextMailService(IConfiguration configuration, DbContextOptions<ApplicationDbContext> options) : base(configuration, options)
        {
        }

        public ApplicationDbContextMailService(IConfiguration configuration, DbSaveChangesInterceptor dbContextAuditLogInterceptor) : base(configuration, dbContextAuditLogInterceptor)
        {
        }

        public ApplicationDbContextMailService(IConfiguration configuration, DbContextOptions<ApplicationDbContext> options, DbSaveChangesInterceptor dbContextAuditLogInterceptor) : base(configuration, options, dbContextAuditLogInterceptor)
        {
        }
    }
}
