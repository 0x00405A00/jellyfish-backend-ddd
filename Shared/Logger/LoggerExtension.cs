using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Shared.Logger
{
    public static class LoggerExtension
    {
        public static void LogDebugForObject<T>(this ILogger logger, Expression<Func<T, string>> id, string prefixString, T obj, [CallerMemberName] string caller = null, [CallerFilePath] string callerFilePath = null, [CallerLineNumber] object callerLineNumber = null)
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
                logger.LogMethod(finalStr,caller,callerFilePath,callerLineNumber);
            }
        }
        /// <summary>
        /// LogError extension
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="exception"></param>
        public static void LogException(this ILogger logger, Exception exception)
        {
            logger.LogError("message={0};stacktrace={1};source={2}", exception.Message, exception.StackTrace, exception.Source);
        }
        /// <summary>
        /// LogDebug extension for method logging
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="caller"></param>
        /// <param name="callerFilePath"></param>
        /// <param name="callerLineNumber"></param>
        public static void LogMethod(this ILogger logger,string message,[CallerMemberName] string caller = null, [CallerFilePath] string callerFilePath=null, [CallerLineNumber] object callerLineNumber = null)
        {
            string callerFileName = null;
            if(!string.IsNullOrEmpty(callerFilePath))
            {
                FileInfo fileInfo = new FileInfo(callerFilePath);
                callerFileName = fileInfo.Name;
            }
            logger.LogDebug("file={0},method={1},line={2},message={3}", callerFileName,caller,callerLineNumber, message);
        }
    }
}
