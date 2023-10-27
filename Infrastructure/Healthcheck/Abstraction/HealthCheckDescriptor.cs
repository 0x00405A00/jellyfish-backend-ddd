using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Infrastructure.Healthcheck.Abstraction
{
    public record HealthCheckDescriptor(Type Type, string Name, HealthStatus FailureStatus, object[] Args);
}
