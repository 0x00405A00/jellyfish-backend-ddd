using Infrastructure.Cache;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics;
using System.Reflection;

namespace Infrastructure.Healthcheck.Concrete.Cache
{
    public class HealthCheckCache : IHealthCheck
    {
        private readonly ICachingHandler _cacheHandler;

        public HealthCheckCache(ICachingHandler cacheHandler)
        {
            _cacheHandler = cacheHandler;
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {

            HealthStatus healthStatus = HealthStatus.Unhealthy;
            string desciption = null;
            Stopwatch stopwatch = Stopwatch.StartNew();
            var responseLocal = _cacheHandler.GetOrSet<object>("test-healthcheck-cache", ()=>Task.FromResult(new object()));
            stopwatch.Stop();
            HealthStatus[] cacheHealthStatus = new HealthStatus[1];
            HealthStatus cacheHealthStatusLocalCache = HealthStatus.Unhealthy;
            if (responseLocal != null)
            {
                cacheHealthStatusLocalCache = stopwatch.ElapsedMilliseconds < 5 ? HealthStatus.Healthy : HealthStatus.Degraded;
            }
            desciption += "local-cache=;Up-state=Up;GET=\"\";fetch-time=0ms;errors=no;warning=no;details=;\n";
            healthStatus = HealthStatus.Unhealthy;
            int countHealthy = cacheHealthStatus.ToList().FindAll(x => x.HasFlag(HealthStatus.Healthy)).Count;
            int countUnHealthy = cacheHealthStatus.ToList().FindAll(x => x.HasFlag(HealthStatus.Unhealthy)).Count;
            int countDegraded = cacheHealthStatus.ToList().FindAll(x => x.HasFlag(HealthStatus.Degraded)).Count;
            int ratioUnHealthy = cacheHealthStatus.Length != 0 ? 100 / cacheHealthStatus.Length * countUnHealthy : 100;
            int ratioDegraded = cacheHealthStatus.Length != 0 ? 100 / cacheHealthStatus.Length * countDegraded : 0;
            if (ratioUnHealthy < 75)
            {
                healthStatus = HealthStatus.Unhealthy;
            }
            else
            {
                healthStatus = HealthStatus.Healthy;
            }
            if (healthStatus == HealthStatus.Unhealthy && cacheHealthStatusLocalCache == HealthStatus.Healthy)
                healthStatus = HealthStatus.Degraded;

            desciption += "whole-check-time=" + stopwatch.ElapsedMilliseconds + "ms;";

            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            AssemblyName currentAssemblyName = currentAssembly.GetName();
            string currentWorkingDir = Directory.GetParent(currentAssembly.Location).FullName;
            /*string libFileName = "Microsoft.Extensions.Caching.StackExchangeRedis.dll";
            Version versionClient = AssemblyName.GetAssemblyName(Path.Combine(currentWorkingDir, libFileName)).Version;

            desciption += "client-version=" + versionClient.ToString() + ";";*/
            return Task.FromResult(new HealthCheckResult(healthStatus, desciption));
        }
    }
}
