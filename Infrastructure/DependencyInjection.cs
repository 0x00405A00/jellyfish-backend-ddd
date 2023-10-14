using Infrastructure.Abstractions;
using Infrastructure.Interceptors;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<DbContextAuditLogInterceptor>();
            services.AddSingleton<ApplicationDbContext>();

            services.AddSingleton<IUnitOfWork,UnitOfWork>();
            return services;    

        }
    }
}
