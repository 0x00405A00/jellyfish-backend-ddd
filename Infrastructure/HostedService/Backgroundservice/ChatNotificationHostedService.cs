using Domain.Primitives;
using Infrastructure.Abstractions;
using Infrastructure.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Infrastructure.Backend.SignalR;
using Shared.Logger;
using System.Data;

namespace Infrastructure.HostedService.Backgroundservice
{
    internal class ChatNotificationHostedService : BackgroundService
    {
        internal static int NotificationSentInterval = 5;
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<ChatNotificationHostedService> _logger;
        private readonly CancellationToken _cancelationToken;

        public ChatNotificationHostedService(
            IServiceProvider serviceProvider,
            ILogger<ChatNotificationHostedService> logger)
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
                _logger.LogDebug($"operation canceled");
            }
        }
        private async Task TExecuteTask()
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var signalRHub = scope.ServiceProvider.GetRequiredService<IHubContext<MessengerHub, IMessengerClient>>();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                using var transaction = dbContext.Database.BeginTransaction();
                {
                    try
                    {
                        if (!MessengerHubExtension.HasConnectedClients)
                        {
                            _logger.LogDebug($"no clients connected for push notifications");
                            return;
                        }

                        _logger.LogDebug($"delivery: {MessengerHubExtension.Connections.Count} clients connected");
                        var messageOutbox = scope.ServiceProvider.GetRequiredService<IMessageOutboxRepository>();
                        var notDeliveredMessages = await messageOutbox.ListWithPessimisticLockAsync(x => MessengerHubExtension.Connections.Values.Contains(x.UserForeignKey));
                        if (!notDeliveredMessages.Any())
                        {
                            _logger.LogDebug($"nothing to deliver");
                            return;
                        }
                        _logger.LogDebug($"delivery: found {notDeliveredMessages.Count} messages for delivering...");
                        var connections = MessengerHubExtension.Connections;
                        var notDeliveredMessagesGroupedByChat = notDeliveredMessages.GroupBy(x => (x.UserForeignKey))
                            .ToDictionary(x => x.Key, y => y.ToList());

                        Parallel.ForEach(notDeliveredMessagesGroupedByChat, x =>
                        {
                            _logger.LogDebug($"delivery:{x.Key}: start to deliver {x.Value.Count} messages to {x.Key}");
                            var messageIdsExtracted = x.Value.Select(x => x.MessageForeignKey.Id).ToList();
                            var connectionIds = MessengerHubExtension.GetConnectionIdByUserId(x.Key).ToList();
                            foreach (var connectionId in connectionIds)
                            {
                                signalRHub.Clients.Client(connectionId).ReceiveMessage(messageIdsExtracted);
                                _logger.LogDebug($"delivery:{x.Key}: completed");
                            }
                        });
                        _logger.LogDebug($"delivery: end, next delivering at {CustomDateTime.Now().DateTime.AddSeconds(NotificationSentInterval)}");
                        _logger.LogDebug($"start committing");
                        transaction.Commit();
                        _logger.LogDebug($"committing successfull");
                    }
                    catch(DBConcurrencyException ex)
                    {
                        _logger.LogException(ex);
                        transaction.Rollback();
                        _logger.LogDebug($"rollback");
                    }
                    catch(Exception ex)
                    {
                        _logger.LogException(ex);
                        transaction.Rollback();
                        _logger.LogDebug($"rollback");
                    }
                }
            }
        }

    }
}
