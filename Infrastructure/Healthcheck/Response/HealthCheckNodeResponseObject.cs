using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json.Serialization;

namespace Infrastructure.Healthcheck.Response
{
    [Serializable]
    public class HealthCheckNodeResponseObject
    {
        [JsonPropertyName("instance-state-str")]
        public string StatusStr => Status.ToString();
        [JsonPropertyName("instance-state")]
        public HealthStatus Status { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("data")]
        public object Data { get; set; }
        [JsonPropertyName("duration")]
        public TimeSpan Duration { get; set; }

        public HealthCheckNodeResponseObject()
        {

        }
        public HealthCheckNodeResponseObject(HealthReportEntry healthReportEntry)
        {
            Status = healthReportEntry.Status;
            Description = healthReportEntry.Description;
            Data = healthReportEntry.Data;
            Duration = healthReportEntry.Duration;
        }
    }
}
