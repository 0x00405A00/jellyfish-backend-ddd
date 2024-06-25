using DotNet.Testcontainers.Containers;
using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Sockets;
using System.Net;
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
                .WithName("db-test")
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
                var connStr = _postgreSqlContainer.GetConnectionString();
                System.Diagnostics.Debug.WriteLine(connStr);
                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(_postgreSqlContainer.GetConnectionString()));

                services.AddDbContext<ApplicationDbContextMailService>(options =>
                options.UseNpgsql(_postgreSqlContainer.GetConnectionString()));
            });
        }

        public async Task InitializeAsync()
        {
            await _postgreSqlContainer.StartAsync();
            await _postgreSqlContainer.WaitForPort();
        }
        public new async Task DisposeAsync()
        {
            await _postgreSqlContainer.StopAsync();
        }
    }
    public static class TestcontainerWorkaround
    {

        public static Task<bool> WaitForPort(this PostgreSqlContainer container, TimeSpan? maxWait = null)
        {
            return WaitForPort(container, PostgreSqlBuilder.PostgreSqlPort, maxWait ?? TimeSpan.FromSeconds(10));
        }

        public static async Task<bool> WaitForPort(this DockerContainer container, int unmappedPort, TimeSpan maxWait)
        {
            var ips = await Dns.GetHostAddressesAsync(container.Hostname);
            if (ips.Length == 0)
            {
                throw new ArgumentException($"Expected minimum 1 IP to resolve from '{container.Hostname}', but got {ips.Length}");
            }

            int portNumber = container.GetMappedPublicPort(unmappedPort);

            CancellationTokenSource ts = new();
            ts.CancelAfter(maxWait);

            using var tcpClient = new TcpClient();

            while (!ts.IsCancellationRequested)
            {
                try
                {
                    await tcpClient.ConnectAsync(ips[0], portNumber, ts.Token);
                    return true;
                }
                catch (SocketException) { }
                await Task.Delay(500, ts.Token);
            }

            return false;
        }
    }
}
