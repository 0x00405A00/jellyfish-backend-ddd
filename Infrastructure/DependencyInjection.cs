﻿using Infrastructure.Abstractions;
using Infrastructure.Healthcheck.Concrete.Cache;
using Infrastructure.Healthcheck.Concrete.MySql;
using Infrastructure.HostedService.Backgroundservice;
using Infrastructure.Interceptors;
using Infrastructure.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.FileSys;
using Infrastructure.Authentification;
using Shared.Authentification.Service;
using Microsoft.Extensions.Hosting;
using Infrastructure.Repository.Concrete;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.Configure<HostOptions>(options =>
            {
                options.ServicesStartConcurrently = true;
                options.ServicesStopConcurrently = true;
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

            services.AddSingleton<IHealtCheckMySqlHandler,HealtCheckMySqlHandler>();

            services.AddScoped<DbSaveChangesInterceptor>();
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMailoutboxRepository, MailOutboxRepository>();

            services.AddScoped<ApplicationDbContextMailService>();
            services.AddScoped<IUnitOfWorkMailService, UnitOfWorkMailService>();
            services.AddScoped<IMailoutboxRepositoryMailService, MailOutboxRepositoryMailService>();//<----- für MailHostedService

            services.AddScoped<IUserRepository, UserRepository>();
            services.Decorate<IUserRepository, CachingUserRepository>();

            services.AddHostedService<MailHostedService>();
            services.AddHostedService<SignalRTestHostedService>();
            services.AddScoped<IMailHandler, MailHandler>();

            services.Scan(selector => selector
                .FromAssemblies(AssemblyReference.Assembly)
                .AddClasses(false)
                .UsingRegistrationStrategy(Scrutor.RegistrationStrategy.Skip)
                .AsImplementedInterfaces()//ClassName => IClassName
                .WithScopedLifetime());

            services.AddHealthChecks()
                .AddCheck<HealthCheckMySql>("mysql")
                .AddCheck<HealthCheckCache>("cache");


            var sp = services.BuildServiceProvider();
            var configuration = sp.GetRequiredService<IConfiguration>();

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
