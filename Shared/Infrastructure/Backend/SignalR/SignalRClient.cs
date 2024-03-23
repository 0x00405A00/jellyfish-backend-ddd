using Microsoft.AspNetCore.SignalR.Client;
using Shared.Const;
using Shared.DataTransferObject;
using Shared.DataTransferObject.Messenger;
using Shared.Infrastructure.Backend.SignalR.Abstraction;

namespace Shared.Infrastructure.Backend.SignalR
{
    public class SignalRClient : AbstractSignalRClient, IMessengerClient, IDisposable
    {
        public static Microsoft.AspNetCore.Connections.TransferFormat SignalRTransferFormat = Microsoft.AspNetCore.Connections.TransferFormat.Text;
        public static Microsoft.AspNetCore.Http.Connections.HttpTransportType SignalRTransportProtocol = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;

        public SignalRClient(
            SignalRConnectionParams signalRConnectionParams,
            ILocalStorageService localStorageService)
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

        }

        public void SignalrClientReconnectedToBackendEvent(object sender, string args)
        {

        }

        public override void InitClientMethods()
        {

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

        public Task ReceiveMessage(List<Guid> MessageIds)
        {
            return Task.CompletedTask;
        }
    }
}
