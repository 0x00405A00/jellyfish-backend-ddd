using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Infrastructure.Logging
{
    public static class SerilogExtension
    {
        public static IHostBuilder UseCentralizedLogging(this IHostBuilder hostBuilder)
        {
            return hostBuilder.UseSerilog((hostingContext, loggerConfiguration) =>
                      loggerConfiguration.ReadFrom
                      .Configuration(hostingContext.Configuration));
        }

        public static ILoggerFactory GetLoggerFactory(IConfiguration configuration)
        {
            var loggerConfiguration = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration.GetSection(Const.SerilogConfigurationSectionKey));

            Log.Logger = loggerConfiguration.CreateLogger();

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.Services.AddSerilog();
            });

            return loggerFactory;
        }
    }
}
