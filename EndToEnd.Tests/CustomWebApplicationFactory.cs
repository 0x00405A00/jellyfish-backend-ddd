using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;
using WebApi;

namespace EndToEnd.Tests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _postgreSqlContainer;

        public CustomWebApplicationFactory()
        {
            _postgreSqlContainer = new PostgreSqlBuilder()
                .WithImage("postgres:latest")
                .Build();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                ReplaceConfigurationWithTestConfig(services);
                ReplaceDbContextWithTestDb(services);
            });
        }

        private void ReplaceDbContextWithTestDb(IServiceCollection services)
        {
            services.RemoveAll<ApplicationDbContext>();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(_postgreSqlContainer.GetConnectionString()));
        }

        private void ReplaceConfigurationWithTestConfig(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var connectionString = _postgreSqlContainer.GetConnectionString();
            configuration["ConnectionStrings:JellyfishMySqlDatabase"] = connectionString;

            services.RemoveAll<IConfiguration>();
            services.AddSingleton<IConfiguration>(configuration);
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
