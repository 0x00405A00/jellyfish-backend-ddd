using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;
using WebApi;

namespace Integration.Tests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup> ,IAsyncLifetime
    {
        private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
                .WithImage("postgres:latest")
                .WithDatabase("jellyfish")
                .WithUsername("jellyfish")
                .WithPassword("meinDatabasePassword!")
                .WithName("db")
                .Build();

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
                services.RemoveAll(typeof(ApplicationDbContext));
                services.RemoveAll(typeof(ApplicationDbContextMailService));

                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(_postgreSqlContainer.GetConnectionString()));

                services.AddDbContext<ApplicationDbContextMailService>(options =>
                options.UseNpgsql(_postgreSqlContainer.GetConnectionString()));
            });
        }

        public async Task InitializeAsync()
        {
            await _postgreSqlContainer.StartAsync();
        }
        public new async Task DisposeAsync()
        {
            await _postgreSqlContainer.StopAsync();
        }
    }
}
