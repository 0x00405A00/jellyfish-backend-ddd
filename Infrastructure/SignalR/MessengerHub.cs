using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

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
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
