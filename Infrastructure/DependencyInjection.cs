using Infrastructure.Abstractions;
using Infrastructure.HostedService.Backgroundservice;
using Infrastructure.Interceptors;
using Infrastructure.Mail;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

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

            return services;    

        }
    }
}
