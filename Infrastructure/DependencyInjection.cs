using Infrastructure.Abstractions;
using Infrastructure.Authentification;
using Infrastructure.Cache;
using Infrastructure.Healthcheck.Concrete.Cache;
using Infrastructure.Healthcheck.Concrete.MySql;
using Infrastructure.HostedService.Backgroundservice;
using Infrastructure.Mail;
using Infrastructure.Repository.Concrete;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Shared.Authentification.Service;
using Shared.Infrastructure.EFCore.Interceptors;
using Shared.Infrastructure.FileSys;
using Shared.Linq.Converters;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            ExpressionFilterExtension.RegisterWellKnownTypes();
            services.Configure<HostOptions>(options =>
            {
                options.ServicesStartConcurrently = true;
                options.ServicesStopConcurrently = true;
            });

            services.AddSerilog();
            services.AddW3CLogging(logging =>
            {
                logging.LoggingFields = W3CLoggingFields.All;
                logging.FlushInterval = TimeSpan.FromSeconds(2);
            });
            services.AddScoped<IAntiVirus, AntiVirus>();
            services.AddScoped<IAzureAdultContentDetection, AzureAdultContentDetection>();

            services.AddScoped<IFileHandler, FileHandler>();
            services.AddSingleton<IFilePermissionService, FilePermissionService>();
            services.AddScoped<IMediaService,MediaService>();

            var serviceBuilder = services.BuildServiceProvider();   
            var fileHandler= serviceBuilder.GetRequiredService<IFileHandler>();
            fileHandler.CreateApplicationFolders();

            services.AddW3CLogging(x => {
                
            });

            services.AddHttpContextAccessor();
            services.AddMemoryCache();//Singleton
            services.AddDistributedMemoryCache(opt => 
            { 

            });//Singleton
            services.AddSingleton<ICachingHandler, CachingHandler>();

            services.AddSingleton<IHealtCheckMySqlHandler,HealtCheckMySqlHandler>();

            var sp = services.BuildServiceProvider();
            var configuration = sp.GetRequiredService<IConfiguration>();
            services.AddScoped<DbSaveChangesInterceptor>();
            services.AddDbContext<ApplicationDbContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMailoutboxRepository, MailOutboxRepository>();
            //services.AddDbContext<ApplicationDbContextMailService>();
            services.AddScoped<IUnitOfWorkMailService, UnitOfWorkMailService>();
            services.AddScoped<IMailoutboxRepositoryMailService, MailOutboxRepositoryMailService>();//<----- für MailHostedService

            services.AddScoped<IUserRepository, UserRepository>();
            services.Decorate<IUserRepository, CachingUserRepository>();
            services.AddScoped<IMessageOutboxRepository, MessageOutboxRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();

            services.AddHostedService<MailHostedService>();
            services.AddHostedService<ChatNotificationHostedService>();
            services.AddScoped<IMailHandler, MailHandler>();

            services.Scan(selector => selector
                .FromAssemblies(AssemblyReference.Assembly)
                .AddClasses(false)
                .UsingRegistrationStrategy(Scrutor.RegistrationStrategy.Skip)
                .AsImplementedInterfaces()//ClassName => IClassName
                .WithScopedLifetime());

            services.AddHealthChecks()
                .AddCheck<HealthCheckMySql>("mysql")
                .AddCheck<HealthCheckCache>("cache")
                .AddCheck<HealthCheckSignalr>("signalr");

            var signalRSection = configuration.GetSection("Infrastructure:SignalR");
            if(signalRSection != null)
            {
                int keepaliveTimeout = signalRSection.GetValue<int>("keepalive_timemout");
                int clientTimeoutSec = signalRSection.GetValue<int>("client_timeout_sec");
                bool debugErrorsDetailedClientside = signalRSection.GetValue<bool>("debug_errors_detailed_clientside");
                int maximumParallelInvocationsPerClient = signalRSection.GetValue<int>("maximum_parallel_invocations_per_per_client");
                int handshakeTimeout = signalRSection.GetValue<int>("handshake_timeout");

                services.AddSignalR(x => {

                    x.KeepAliveInterval = TimeSpan.FromSeconds(keepaliveTimeout);
                    x.ClientTimeoutInterval = TimeSpan.FromSeconds(clientTimeoutSec);
                    x.EnableDetailedErrors = debugErrorsDetailedClientside;
                    x.MaximumParallelInvocationsPerClient = maximumParallelInvocationsPerClient;
                    x.HandshakeTimeout = TimeSpan.FromSeconds(handshakeTimeout);

                }).AddJsonProtocol(options =>
                {
                    options.PayloadSerializerOptions.PropertyNamingPolicy = null;
                });

            }
            return services;    

        }
    }
}
