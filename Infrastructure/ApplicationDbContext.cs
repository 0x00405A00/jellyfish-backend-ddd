using Domain.Entities.Auth;
using Domain.Entities.Chats;
using Domain.Entities.MailoutBox;
using Domain.Entities.Message;
using Domain.Entities.Role;
using Domain.Entities.User;
using Domain.ValueObjects;
using Infrastructure.DatabaseEntityConfiguration;
using Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Infrastructure
{
    public class ApplicationDbContext: DbContext
    {
        #region Const
        public const string ConnectionStringAlias = "JellyfishMySqlDatabase";
        public struct ColumnLength
        {
            public const int Ids = 36;
            public const int Names = 64;
            public const int Descriptions = 255;
        }
        public static Func<Type, string> GetTableName = new Func<Type, string>((type) =>
        {
            string typeName = type.Name;
            string tableName = null;
            StringBuilder result = new StringBuilder();

            foreach (char character in typeName)
            {
                if (char.IsUpper(character))
                {
                    result.Append('_');
                }
                result.Append(character);
            }
            tableName = result.ToString().TrimStart('_');

            return ($"{tableName}").ToLower(); 
        });
        public static Func<string, string> GetIndexName = new Func<string, string>((name) => 
        { return ($"IDX_{name}").ToUpper(); });
        public static Func<string, string, string> GetForeignKeyName = new Func<string,string, string>((from,to) => 
        { return ($"FK_{from}_TO_{to}_{Guid.NewGuid()}").ToUpper(); });

        #endregion
        #region Consumed DI Services
        public DbSaveChangesInterceptor DbContextAuditLogInterceptor { get; }
        public IConfiguration _configuration { get; }
        public DbContextOptions<ApplicationDbContext> Options { get; }
        #endregion
        #region DbSets

        #endregion
        #region Ctor
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
            var connectionString = _configuration.GetConnectionString(ConnectionStringAlias);
            optionsBuilder.UseMySQL(connectionString, x => {

            });
            optionsBuilder.AddInterceptors(DbContextAuditLogInterceptor);
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
            optionsBuilder.ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
            optionsBuilder.EnableSensitiveDataLogging(true);
        }

#warning Before any start, check if u changed the entity structure in IEntityConfiguration classes. When any changes are done, please migrate to database 
#warning Documentation: https://learn.microsoft.com/de-de/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //pay attention to dependency order (chicken egg problem)
            modelBuilder.ApplyConfiguration<UserType>(new UserTypeConfiguration());
            modelBuilder.ApplyConfiguration<User>(new UserConfiguration());
            modelBuilder.ApplyConfiguration<UserFriend>(new UserFriendConfiguration());
            modelBuilder.ApplyConfiguration<FriendshipRequest>(new UserFriendshipRequestConfiguration());
            modelBuilder.ApplyConfiguration<Role>(new RoleConfiguration());
            modelBuilder.ApplyConfiguration<UserRole>(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration<Auth>(new AuthConfiguration());
            modelBuilder.ApplyConfiguration<Chat>(new ChatConfiguration());
            modelBuilder.ApplyConfiguration<ChatMember>(new ChatMemberConfiguration());
            modelBuilder.ApplyConfiguration<Message>(new ChatMessageConfiguration());
            modelBuilder.ApplyConfiguration<EmailType>(new EmailTypeConfiguration());
            modelBuilder.ApplyConfiguration<MailOutbox>(new MailConfiguration());
            modelBuilder.ApplyConfiguration<MailOutboxRecipient>(new MailRecipientConfiguration());
            modelBuilder.ApplyConfiguration<MailOutboxAttachment>(new MailAttachmentConfiguration());

            //modelBuilder.ApplyConfigurationsFromAssembly ignore any order so dependencies which are order depend could not be created (app runs in exception)

            //Data seeding: Schema initial data with: https://learn.microsoft.com/de-de/ef/core/modeling/data-seeding
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
