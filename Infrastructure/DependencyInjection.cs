using Application.Shared.Kernel.Web.AspNet.Healthcheck;
using Infrastructure.Abstractions;
using Infrastructure.Healthcheck.Abstraction;
using Infrastructure.HostedService.Backgroundservice;
using Infrastructure.Interceptors;
using Infrastructure.Mail;
using Infrastructure.Repository;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddMemoryCache();

            services.AddScoped<DbContextAuditLogInterceptor>();
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMailoutboxRepository, MailOutboxRepository>();

            services.AddScoped<ApplicationDbContextMailService>();
            services.AddScoped<IUnitOfWorkMailService, UnitOfWorkMailService>();
            services.AddScoped<IMailoutboxRepositoryMailService, MailOutboxRepositoryMailService>();//<----- für MailHostedService

            services.AddScoped<IUserRepository, UserRepository>();
            services.Decorate<IUserRepository, CachingUserRepository>();

            services.AddHostedService<MailHostedService>();
            services.AddScoped<IMailHandler, MailHandler>();

            services.Scan(selector => selector
            .FromAssemblies(AssemblyReference.Assembly)
            .AddClasses(false)
            .UsingRegistrationStrategy(Scrutor.RegistrationStrategy.Skip)
            .AsImplementedInterfaces()//ClassName => IClassName
            .WithScopedLifetime());

            var sp = services.BuildServiceProvider();
            var configuration = sp.GetRequiredService<IConfiguration>();
            var cacheService = sp.GetRequiredService<IMemoryCache>();
            var databaseSingletonService = sp.GetRequiredService<ApplicationDbContext>();

            services.AddHealthChecks()
                .AddHealthChecks(new List<HealthCheckDescriptor>()
                {
                    new HealthCheckDescriptor(typeof(HealthCheckMySql),"mysql", HealthStatus.Unhealthy, new object[]
                    {
                        HealthCheckMySql.CheckMySqlBackend, databaseSingletonService
                    })
                })
                .AddHealthChecks(new List<HealthCheckDescriptor>()
                {
                    new HealthCheckDescriptor(typeof(HealthCheckCache),"cache", HealthStatus.Unhealthy, new object[]
                    {
                        HealthCheckCache.CheckCacheBackend, cacheService
                    })
                });

            var healthCheckService = sp.GetService<HealthCheckService>();
            var resultHealthCheck = async () => {
                var result = await healthCheckService.CheckHealthAsync();
                foreach (var item in result.Entries)
                {
                    Console.WriteLine("[HealthCheckService]: " + item.Key + ": " + item.Value.Description + "");
                }
            };
            resultHealthCheck.Invoke();




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
