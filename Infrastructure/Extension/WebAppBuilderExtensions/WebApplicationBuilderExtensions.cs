using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extension.WebAppBuilderExtensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static IApplicationBuilder AppendMigrations(this IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            using ApplicationDbContext applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            if (applicationDbContext.Database.EnsureCreated())
            {
                var migrations = applicationDbContext.Database.GetPendingMigrations();
                if(migrations.Any())
                {
                    applicationDbContext.Database.Migrate();
                }
            }
            return app;
        }
    }
}
