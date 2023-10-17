using Infrastructure.Abstractions;
using Infrastructure.Interceptors;
using Infrastructure.Mapper;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddMemoryCache();  
            services.AddScoped<IUserRepository, UserRepository>();
            services.Decorate<IUserRepository, CachingUserRepository>();

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
