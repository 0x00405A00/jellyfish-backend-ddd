using Shared.Infrastructure.EFCore.DatabaseEntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Shared.Infrastructure.EFCore
{
    public static class ModelBuilderExtension
    {
        private static bool _isSampleDataLoaded = false;
        public static ModelBuilder CreateModels(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserHasRelationToFriendsConfiguration());
            modelBuilder.ApplyConfiguration(new FriendshipRequestsConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserHasRelationToRoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
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

            /*modelBuilder.Entity<Email>()
                .HasNoKey() // keyless
                .ToView(null);*/
            return modelBuilder;
        }
        public static ModelBuilder CreateInitialDataSeed(this ModelBuilder modelBuilder)
        {
            modelBuilder.CreateUserTypes();
            modelBuilder.CreateSystemUser();
            modelBuilder.CreateEmailSendingTypes();

            return modelBuilder;
        }
        public static ModelBuilder CreateSampleData(this ModelBuilder modelBuilder)
        {
            modelBuilder.SampleUsers();
            modelBuilder.SampleChats();
            //dotnet ef tool doesnt take consideration of conditions, i dont know why (bug)
            //-> dotnet ef database update --project .\Infrastructure.csproj --context ApplicationDbContext
            //Usual the method calls "modelBuilder.SampleUsers();" and "modelBuilder.SampleChats();" are inside the below code block in reason of a ef core bug its moved out of the block upper here
            if (!IsSampleDataLoaded())
            {
                _isSampleDataLoaded = true;
            }
            return modelBuilder;
        }
        public static ModelBuilder CreateOwnedTypes(this ModelBuilder modelBuilder)
        {

            return modelBuilder;
        }
        public static bool IsSampleDataLoaded() => _isSampleDataLoaded;
    }
}
