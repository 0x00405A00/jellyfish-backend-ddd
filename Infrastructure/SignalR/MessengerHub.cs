using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Shared.Infrastructure.SignalR;

namespace Infrastructure.SignalR
{
    public class MessengerHub : Hub<IMessengerClient>
    {
        private readonly ILogger<MessengerHub> logger;

        public MessengerHub(ILogger<MessengerHub> logger)
        {
            this.logger = logger;
        }

        public override Task OnConnectedAsync()
        {

            logger.LogInformation($"Client connected: {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            logger.LogInformation($"Client disconnected: {Context.ConnectionId}");
            if (exception != null)
            {
                logger.LogError($"Exception: {exception.Message}");
            }
            return base.OnDisconnectedAsync(exception);
        }
    }
}
