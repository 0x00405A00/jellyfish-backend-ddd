using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Infrastructure.Healthcheck.Response
{
    [Serializable]
    public class HealthCheckResponseObject
    {
        private int _healthyCount = 0;
        private int _unhealthyCount = 0;
        private int _degradedCount = 0;
        [JsonPropertyName("general-state")]
        public HealthStatus Status
        {
            get;
            private set;
        }
        [JsonPropertyName("general-state-str")]
        public string StatusStr => Status.ToString();
        [JsonPropertyName("start-check-datetime")]
        public string StartCheckDateTime { get; private set; }
        [JsonPropertyName("completed-check-datetime")]
        public string CompletedCheckDateTime { get; private set; }
        [JsonPropertyName("checks-count")]
        public int Count => HealthCheckChain.Keys.Count;
        [JsonPropertyName("healthy-checks-count")]
        public int HealthyCount => _healthyCount;
        [JsonPropertyName("unhealthy-checks-count")]
        public int UnHealthyCount => _unhealthyCount;
        [JsonPropertyName("degraded-checks-count")]
        public int DegradedCount => _degradedCount;
        [JsonPropertyName("healthy-checks-count-percentage")]
        public double HealthyCountPercent => Math.Round(100.0 / Count * _healthyCount, 2);
        [JsonPropertyName("unhealthy-checks-count-percentage")]
        public double UnHealthyCountPercent => Math.Round(100.0 / Count * _unhealthyCount, 2);
        [JsonPropertyName("degraded-checks-count-percentage")]
        public double DegradedCountPercent => Math.Round(100.0 / Count * _degradedCount, 2);

        [JsonPropertyName("pid")]
        public int ProcessId { get; } = Environment.ProcessId;
        [JsonPropertyName("process-start-time")]
        public DateTime ProcessStartTime { get; set; } = DateTime.MinValue;
        [JsonPropertyName("working-directory")]
        public string WorkingDir { get; } = Environment.CurrentDirectory;
        [JsonPropertyName("operating-system")]
        public string OS { get; } = Environment.OSVersion.VersionString;
        [JsonPropertyName("net-bios-name")]
        public string Hostname { get; } = Environment.MachineName;
        [JsonPropertyName("processor-count")]
        public int Processors { get; } = Environment.ProcessorCount;
        [JsonPropertyName("environment-vars")]
        public IDictionary<string, string?> EnvironmentVars { get; set; } = null;
        [JsonPropertyName("process-args")]
        public string ProcessArguments { get; set; } = null;
        [JsonPropertyName("processor-usage-percentage-all")]
        public double ProcessorUsageAll { get; set; } = 0;
        [JsonPropertyName("processor-usage-percentage")]
        public double ProcessorUsage { get; set; } = 0;
        [JsonPropertyName("ram-system-mb")]
        public double RamSystem { get; set; } = 0;
        [JsonPropertyName("ram-used-mb")]
        public double RamUsage { get; set; } = 0;
        [JsonPropertyName("ram-used-percentage")]
        public double RamUsagePercentage
        {
            get
            {
                return 100.0 / RamSystem * RamUsage;
            }
        }
        [JsonPropertyName("host-sys-user")]
        public string AppHostUser { get; } = Environment.UserName;
        [JsonPropertyName("app-name")]
        public string AppName { get; } = Assembly.GetExecutingAssembly().GetName().Name;
        [JsonPropertyName("build-version")]
        public string Build { get; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        [JsonPropertyName("app-fullname")]
        public string AppFullName { get; } = Assembly.GetExecutingAssembly().GetName().FullName.ToString();
        [JsonPropertyName("encoding")]
        public string Encoding { get; } = System.Text.Encoding.Default.WebName;

        [JsonPropertyName("checks")]
        public Dictionary<string, HealthCheckNodeResponseObject> HealthCheckChain { get; set; }

        public HealthCheckResponseObject()
        {
            HealthCheckChain = new Dictionary<string, HealthCheckNodeResponseObject>();
        }

        public void RefreshStatistics()
        {

            int c = HealthCheckChain.Keys.Count;
            var valuesL = HealthCheckChain.Values.ToList();
            _healthyCount = valuesL.Count(x => x.Status == HealthStatus.Healthy);
            _unhealthyCount = valuesL.Count(x => x.Status == HealthStatus.Unhealthy);
            _degradedCount = valuesL.Count(x => x.Status == HealthStatus.Degraded);

            Status = HealthyCountPercent == 100.0 ? HealthStatus.Healthy : HealthStatus.Unhealthy;
        }
    }
}
