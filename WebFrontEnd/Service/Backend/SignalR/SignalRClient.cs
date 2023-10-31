
using Infrastructure.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Shared.DataTransferObject;
using Shared.DataTransferObject.Messenger;
using WebFrontEnd.Service.Backend.SignalR.Abstraction;
using WebFrontEnd.Service.WebStorage.LocalStorage;
using WebFrontEnd.Const;

namespace WebFrontEnd.Service.Backend.SignalR
{

    public class SignalRClient : AbstractSignalRClient, IMessengerClient
    {
        public static Microsoft.AspNetCore.Connections.TransferFormat SignalRTransferFormat = Microsoft.AspNetCore.Connections.TransferFormat.Text;
        public static Microsoft.AspNetCore.Http.Connections.HttpTransportType SignalRTransportProtocol = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;

        public SignalRClient(
            IConfiguration configuration,
            ILocalStorageService localStorageService)
        {
            var infrastructureSignalRSection = configuration.GetSection("Infrastructure:SignalR");
            var baseUrl = infrastructureSignalRSection.GetValue<string>("SignalRHubBaseUrl");
            var port = infrastructureSignalRSection.GetValue<string>("SignalRHubBaseUrlPort");
            var endpoint = infrastructureSignalRSection.GetValue<string>("SignalRHubEndpoint");
            var protocol = infrastructureSignalRSection.GetValue<int>("SignalRHubClientTransportProtocol");


            string url = baseUrl + ":" +
                port +
                endpoint;
            

            this.Initialize(url, async () =>
            {
                var token = await localStorageService.GetDeserializedJsonItemFromKey<AuthDTO>(WebAppAuthorizationConst.BrowserLocalStorageItemKey.Authorization);
                return token.Token;
            },
            SignalRTransportProtocol, SignalRTransferFormat
            );

            this.HubConnectionReconnectedEvent += new EventHandler<string>(SignalrClientReconnectedToBackendEvent);

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

        public Task ReceiveMessage(List<MessageDTO> messages)
        {
            return Task.CompletedTask;
        }
    }
}
