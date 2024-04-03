using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Shared.Logger
{
    public static class LoggerExtension
    {
        public static void LogDebugForObject<T>(this ILogger logger, Expression<Func<T, string>> id, string prefixString, T obj)
        {
            var props = obj.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.GetProperty);
            var method = id.Compile();
            string idStr = method.Invoke(obj);
            foreach (var prop in props)
            {
                string propertyName = prop.Name;
                string propertyType = prop.DeclaringType.Name;
                var propertyValue = prop.GetValue(obj, null);
                string finalStr = string.Format("{0}:{1}:property:{2} {3}:{4}", prefixString, idStr, propertyType, propertyName, propertyValue);
                logger.LogDebug(finalStr);
            }
        }
        public static void LogException(this ILogger logger, Exception exception)
        {
            logger.LogError("message={0};stacktrace={1};source={2}",exception.Message,exception.StackTrace, exception.Source);
        }
    }
}
