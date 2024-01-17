using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.Infrastructure.Backend;

namespace WebFrontEnd.BackgroundService
{
    public class LogoutBackgroundService : Microsoft.Extensions.Hosting.BackgroundService
    {
        public event Action OnRerenderRequested;
        public static TimeSpan Period = new TimeSpan(0,0,1);
        private readonly ILogger<LogoutBackgroundService> logger;
        private readonly IServiceProvider serviceProvider;

        public LogoutBackgroundService(
            ILogger<LogoutBackgroundService> logger, 
            IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using(var scope =  serviceProvider.CreateScope())
            {
                var navigationManager = scope.ServiceProvider.GetService<NavigationManager>();
                var localStorageService = scope.ServiceProvider.GetService<ILocalStorageService>();
                var jsRunTime = scope.ServiceProvider.GetService<IJSRuntime>();
                using var periodicTimer = new PeriodicTimer(Period);
                do
                {
                    logger.LogInformation("LogoutBackgroundService elapsed");
                    OnRerenderRequested?.Invoke();
                }
                while (!stoppingToken.IsCancellationRequested && await periodicTimer.WaitForNextTickAsync(stoppingToken));
            }

        }
    }
}
