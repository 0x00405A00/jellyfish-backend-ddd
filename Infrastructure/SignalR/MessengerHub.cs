using Domain.Extension;
using Domain.Primitives.Ids;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Shared.Infrastructure.Backend.SignalR;

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
            logger.LogInformation($"client:{Context.ConnectionId}:connection:start");
            var userUuid = Context.GetUserUuidFromRequest();
            if(userUuid == Guid.Empty)
            {
                logger.LogInformation($"client:{Context.ConnectionId}:connection:start:without identity");
            }

            MessengerHubExtension.AddConnection(Context.ConnectionId,userUuid.ToIdentification<UserId>());
            logger.LogInformation($"client:{Context.ConnectionId}:connection:established");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            logger.LogInformation($"client:{Context.ConnectionId}:connection:begin closing");
            var userUuid = Context.GetUserUuidFromRequest();
            MessengerHubExtension.RemoveConnection(Context.ConnectionId);
            if (exception != null)
            {
                logger.LogError($"client:{Context.ConnectionId}:connection:begin closing:exception happened");
                logger.LogError($"exception: {exception.Message}");
            }
            logger.LogInformation($"client:{Context.ConnectionId}:connection:disconnected");
            return base.OnDisconnectedAsync(exception);
        }
    }
}
