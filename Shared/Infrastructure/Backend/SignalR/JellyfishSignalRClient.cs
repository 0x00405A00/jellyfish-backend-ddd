using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Shared.Const;
using Shared.DataTransferObject;
using Shared.DataTransferObject.Messenger;
using Shared.Infrastructure.Backend.Api;
using Shared.Infrastructure.Backend.Interceptor.Abstraction;
using Shared.Infrastructure.Backend.SignalR.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Infrastructure.Backend.SignalR
{
    public class JellyfishSignalRClient : AbstractSignalRClient, IMessengerClient, IDisposable
    {
        public static Microsoft.AspNetCore.Connections.TransferFormat SignalRTransferFormat = Microsoft.AspNetCore.Connections.TransferFormat.Text;
        public static Microsoft.AspNetCore.Http.Connections.HttpTransportType SignalRTransportProtocol = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
        private readonly ILogger<JellyfishSignalRClient> logger;
        private readonly IInternalDataInterceptorApplicationDispatcher internalDataInterceptorApplicationDispatcher;
        private readonly IJellyfishBackendApi jellyfishBackendApi;


        public JellyfishSignalRClient(
            ILogger<JellyfishSignalRClient> logger,
            SignalRConnectionParams signalRConnectionParams,
            IInternalDataInterceptorApplicationDispatcher internalDataInterceptorApplicationDispatcher,
            ILocalStorageService localStorageService,
            IJellyfishBackendApi jellyfishBackendApi)
        {

            string url = signalRConnectionParams.GetConnectionUrl();


            this.Initialize(url, async () =>
            {
                var token = await localStorageService.GetDeserializedJsonItemFromKey<AuthDTO>(AuthorizationConst.SessionStorage.AuthorizationKey);
                return token?.Token;
            },
            SignalRTransportProtocol, SignalRTransferFormat
            );

            HubConnectionReconnectedEvent += new EventHandler<string>(SignalrClientReconnectedToBackendEvent);
            this.logger = logger;
            this.internalDataInterceptorApplicationDispatcher = internalDataInterceptorApplicationDispatcher;
            this.jellyfishBackendApi = jellyfishBackendApi;
        }

        public void SignalrClientReconnectedToBackendEvent(object sender, string args)
        {

        }

        public override void InitClientMethods()
        {
            HubConnection.On<List<Guid>>(nameof(ReceiveMessage), ReceiveMessage);
            HubConnection.On<UserDTO>(nameof(AcceptFriendshipRequest), AcceptFriendshipRequest);
            HubConnection.On<UserFriendshipRequestDTO>(nameof(ReceiveFriendshipRequest), ReceiveFriendshipRequest);
        }

        public Task AcceptFriendshipRequest(UserDTO userDTO)
        {
            return Task.CompletedTask;
        }

        public Task ReceiveFriendshipRequest(UserFriendshipRequestDTO request)
        {
            return Task.CompletedTask;
        }

        private HashSet<Guid> MessageIdQueue = new HashSet<Guid>();
        public async Task ReceiveMessage(List<Guid> MessageIds)
        {
            Guid[] tmp = new Guid[MessageIds.Count];
            MessageIds.CopyTo(tmp);
            var tmpAsList = tmp.ToList();   
            foreach (var messageId in MessageIds)
            {
                if(MessageIdQueue.Contains(messageId))
                {
                    tmpAsList.Remove(messageId);
                }
            }
            if(tmpAsList.Any())
            {
                var notDeliveredMessagesResponse = await jellyfishBackendApi.GetNotDeliveredMessages(CancellationToken.None);
                if(notDeliveredMessagesResponse.IsSuccess && notDeliveredMessagesResponse.Data.Any())
                {
                    var responseProccessing = await internalDataInterceptorApplicationDispatcher.ReceiveMessage(notDeliveredMessagesResponse.Data.ToArray());
                    MessageIdQueue.Clear();
                }
            }
        }
    }
}
