using Infrastructure.Healthcheck.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure.Healthcheck.Extension
{
    public static class HealthChecksExtensions
    {
        public static readonly string AddTypeActivatedCheckMethod = "AddTypeActivatedCheck";
        public static readonly int AddTypeActivatedCheckMethodParamLen =4;

        public static IHealthChecksBuilder AddHealthChecks(this IHealthChecksBuilder healthChecksBuilder, List<HealthCheckDescriptor> healthCheckDescriptors)
        {
            foreach (var healthCheckDescriptor in healthCheckDescriptors)
            {
                var methodConvertedModel = typeof(HealthChecksBuilderAddCheckExtensions).GetMethods(BindingFlags.Static | BindingFlags.Public).ToList().Find(x => x.Name == AddTypeActivatedCheckMethod && x.GetParameters().Length == AddTypeActivatedCheckMethodParamLen);
                var m = methodConvertedModel.MakeGenericMethod(healthCheckDescriptor.Type);
                healthChecksBuilder = (IHealthChecksBuilder)m.Invoke(healthChecksBuilder, new object[] { healthChecksBuilder, "healthcheck_" + healthCheckDescriptor.Name, healthCheckDescriptor.FailureStatus, healthCheckDescriptor.Args });


            }
            return healthChecksBuilder;
        }
    }
}
