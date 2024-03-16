using System.Reflection;
using System.Text.Json.Serialization;
using Shared.DataTransferObject.Abstraction;

namespace Shared.DataTransferObject
{
    [Serializable]  
    public class HealthZDTO : IDataTransferObject
    {

        [JsonPropertyName("general-state")]
        public int Status { get; set; }
        [JsonPropertyName("general-state-str")]
        public string StatusStr { get; set; }
        [JsonPropertyName("start-check-datetime")]
        public string StartCheckDateTime { get; set; }
        [JsonPropertyName("completed-check-datetime")]
        public string CompletedCheckDateTime { get; set; }
        [JsonPropertyName("checks-count")]
        public int Count { get; set; }
        [JsonPropertyName("healthy-checks-count")]
        public int HealthyCount { get; set; }
        [JsonPropertyName("unhealthy-checks-count")]
        public int UnHealthyCount { get; set; }
        [JsonPropertyName("degraded-checks-count")]
        public int DegradedCount { get; set; }
        [JsonPropertyName("healthy-checks-count-percentage")]
        public double HealthyCountPercent { get; set; }
        [JsonPropertyName("unhealthy-checks-count-percentage")]
        public double UnHealthyCountPercent { get; set; }
        [JsonPropertyName("degraded-checks-count-percentage")]
        public double DegradedCountPercent { get; set; }

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
        public double RamUsagePercentage { get; set; } = 0;
        [JsonPropertyName("host-sys-user")]
        public string AppHostUser { get; } = Environment.UserName;
        [JsonPropertyName("app-name")]
        public string AppName { get; } = null;
        [JsonPropertyName("build-version")]
        public string Build { get; } = null;
        [JsonPropertyName("app-fullname")]
        public string AppFullName { get; } = null;
        [JsonPropertyName("encoding")]
        public string Encoding { get; } = null;

        [JsonPropertyName("checks")]
        public Checks Checks { get; set; }
    }

    [Serializable]
    public class Checks
    {
        public Mysql mysql { get; set; }
        public Cache cache { get; set; }
        public Signalr signalr { get; set; }
    }
    public abstract class Check
    {
        public string instancestatestr { get; set; }
        public int instancestate { get; set; }
        public string description { get; set; }
        public object data { get; set; }
        public string duration { get; set; }
        public string[] GetFormattedDescription(string separator = ";")=>description?.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }
    [Serializable]
    public class Mysql : Check
    {
    }

    [Serializable]
    public class Cache : Check
    {
    }
    [Serializable]
    public class Signalr : Check
    {
    }


}
