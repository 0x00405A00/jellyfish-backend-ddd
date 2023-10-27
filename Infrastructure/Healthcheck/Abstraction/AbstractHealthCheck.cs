using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Infrastructure.Healthcheck.Abstraction
{
    public abstract class AbstractHealthCheck<T1, T2> : IHealthCheck
        where T2 : Task<HealthCheckResult>
    {


        public readonly Func<T1, T2> CheckHealthAction;
        private ReadOnlyDictionary<string, object> _properties;

        protected ReadOnlyDictionary<string, object> PropertiesViaCtor
        {
            get
            {
                return _properties;
            }
        }
        public AbstractHealthCheck(Func<T1, T2> checkHealthAction)
        {
            CheckHealthAction = checkHealthAction;
        }
        public AbstractHealthCheck(Func<T1, T2> checkHealthAction, Dictionary<string, object> data) : this(checkHealthAction)
        {
            _properties = new ReadOnlyDictionary<string, object>(data);
        }
        public abstract Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default);
        public ReadOnlyDictionary<string, object> GetInstanceProps(object instance)
        {
            var dict = new Dictionary<string, object>();
            List<PropertyInfo> properties = GetType().GetProperties().ToList();
            foreach (PropertyInfo property in properties)
            {
                var methodInfoSetter = property.GetSetMethod();
                bool isPublic = methodInfoSetter != null;
                if (!isPublic)
                    continue;
                string key = property.Name;
                object value = property.GetValue(instance);
                dict.Add(key, value);
            }
            var dictR = new ReadOnlyDictionary<string, object>(dict);
            return dictR;
        }
    }
}
