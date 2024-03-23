using Infrastructure.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Shared.Infrastructure.Backend.SignalR;
using System.Diagnostics;

namespace Infrastructure.Healthcheck.Concrete.Cache
{
    public class HealthCheckSignalr : IHealthCheck
    {
        private readonly IServiceProvider serviceProvider;

        public HealthCheckSignalr(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {

            using (var scope = serviceProvider.CreateScope())
            {
                HealthStatus healthStatus = HealthStatus.Healthy;
                string desciption = null;
                Stopwatch stopwatch = Stopwatch.StartNew();

                stopwatch.Stop();
                HealthStatus[] cacheHealthStatus = new HealthStatus[1];

                var connectionCount = MessengerHubExtension.Connections.Count();
                desciption += $"local-signalr=;Up-state=Up;connection-count={connectionCount};fetch-time=0ms;errors=no;warning=no;details=;\n";
                return Task.FromResult(new HealthCheckResult(healthStatus, desciption));

            }
        }
    }
}
