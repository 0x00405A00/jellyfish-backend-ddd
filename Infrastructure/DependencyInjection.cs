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
            services.AddMemoryCache();  
            services.AddSingleton<IMailoutboxRepository,MailOutboxRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.Decorate<IUserRepository, CachingUserRepository>();

            services.AddHostedService<MailHostedService>();
            services.AddSingleton<IMailHandler, MailHandler>();

            services.Scan(selector => selector
            .FromAssemblies(AssemblyReference.Assembly)
            .AddClasses(false)
            .UsingRegistrationStrategy(Scrutor.RegistrationStrategy.Skip)
            .AsImplementedInterfaces()//ClassName => IClassName
            .WithScopedLifetime());

            services.AddHttpContextAccessor();
            services.AddSingleton<DbContextAuditLogInterceptor>();
            services.AddSingleton<ApplicationDbContext>();


            services.AddSingleton<IUnitOfWork,UnitOfWork>();
            return services;    

        }
    }
}
