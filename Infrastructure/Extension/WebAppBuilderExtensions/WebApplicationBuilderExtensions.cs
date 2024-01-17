using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extension.WebAppBuilderExtensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplication AppendMigrations(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            using ApplicationDbContext applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            if (applicationDbContext.Database.EnsureCreated())
            {
                applicationDbContext.Database.Migrate();
            }
            return app;
        }
    }
}
