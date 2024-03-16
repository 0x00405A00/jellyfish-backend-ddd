using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Shared.Infrastructure.Backend.SignalR;
using SharpCompress;

namespace Infrastructure.SignalR
{
    public class MessengerHubExtension
    {
        private static HashSet<string> _connectionsIds = new HashSet<string>();
        public static ReadOnlyCollection<string> ConnectionsIds => _connectionsIds.ToReadOnly();

        public static void AddConnection(string connectionId) => _connectionsIds.Add(connectionId);
        public static void RemoveConnection(string connectionId) => _connectionsIds.Remove(connectionId);
    }
    public class MessengerHub : Hub<IMessengerClient>
    {
        private readonly ILogger<MessengerHub> logger;

        public MessengerHub(ILogger<MessengerHub> logger)
        {
            this.logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            MessengerHubExtension.AddConnection(Context.ConnectionId);
            logger.LogInformation($"Client connected: {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            MessengerHubExtension.RemoveConnection(Context.ConnectionId);
            logger.LogInformation($"Client disconnected: {Context.ConnectionId}");
            if (exception != null)
            {
                logger.LogError($"Exception: {exception.Message}");
            }
            return base.OnDisconnectedAsync(exception);
        }
    }
}
