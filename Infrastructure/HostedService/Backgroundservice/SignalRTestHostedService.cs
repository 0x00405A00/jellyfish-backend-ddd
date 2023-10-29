using Infrastructure.Abstractions;
using Infrastructure.Mail;
using Infrastructure.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.Text;

namespace Infrastructure.HostedService.Backgroundservice
{
    internal class SignalRTestHostedService : BackgroundService
    {
        internal static int NotificationSentInterval = 5;
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<SignalRTestHostedService> _logger;
        private readonly CancellationToken _cancelationToken;

        public SignalRTestHostedService(
            IServiceProvider serviceProvider,
            ILogger<SignalRTestHostedService> logger)
        {
            this.serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {


            using PeriodicTimer timer = new(TimeSpan.FromSeconds(NotificationSentInterval));

            try
            {
                do
                {
                    await TExecuteTask();
                }
                while (await timer.WaitForNextTickAsync(stoppingToken));
            }
            catch (OperationCanceledException)
            {

            }
        }
        private async Task TExecuteTask()
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var signalRHub = scope.ServiceProvider.GetRequiredService<IHubContext<MessengerHub, IMessengerClient>>();
                await signalRHub.Clients.All.ReceiveMessage(new List<Shared.DataTransferObject.Messenger.MessageDTO> { new Shared.DataTransferObject.Messenger.MessageDTO { Text="TTT" } });
                
            }
        }

    }
}
