using Infrastructure;
using Infrastructure.Mail;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EndToEnd.Tests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                // Clear default configuration sources
                //config.Sources.Clear();

                // Set the base path to the project directory
                /*var projectDir = Directory.GetCurrentDirectory();
                var configPath = Path.Combine(projectDir, "appsettings.json");

                config.AddJsonFile(configPath, optional: false, reloadOnChange: true);*/

                // Add other configuration sources if necessary
            });

            builder.ConfigureServices(services =>
            {
                // Add test-specific services if needed
                var sP = services.BuildServiceProvider();
                var service1 = sP.GetRequiredService<ApplicationDbContext>();
                var service2 = sP.GetRequiredService<IMailHandler>();
                var service3 = sP.GetRequiredService<IHostEnvironment>();
            });
        }

        protected override IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<TStartup>();
                });
        }
    }
}
