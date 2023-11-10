
using Microsoft.AspNetCore.SignalR.Client;
using MobileApp.Handler.AppConfig;
using MobileApp.ViewModel;
#if ANDROID
using MobileApp.Handler.Data.InternalDataInterceptor.Invoker.Notification.Android;
#elif IOS
using MobileApp.Handler.Data.InternalDataInterceptor.Invoker.Notification.iOS;
#endif

namespace MobileApp.Handler.Backend.Communication.SignalR
{
    public class SignalRClient : AbstractSignalRClient, IMessengerClient
    {
        public List<BaseViewModel> ViewModelsToCommunicate { get; set; }
        private readonly MobileApp.Handler.Data.InternalDataInterceptor.InternalDataInterceptorApplication _messageDataInterceptor;

        public SignalRClient(
            MobileApp.Handler.Data.InternalDataInterceptor.InitDataInterceptorApplicationModel initDataInterceptorApplicationModel,
            ApplicationConfigHandler applicationConfigHandler,
            MobileApp.Handler.Data.InternalDataInterceptor.InternalDataInterceptorApplication messageDataInterceptor) : base()
        {
            string protocolSignalR = applicationConfigHandler.ApplicationConfig.NetworkConfig.WebApiHttpClientTransportProtocol == MobileApp.Data.AppConfig.ConcreteImplements.NetworkConfig.HTTP_TRANSPORT_PROTOCOLS.HTTP ? "http://" : "https://";
            string url =
            protocolSignalR +
                applicationConfigHandler.ApplicationConfig.NetworkConfig.SignalRHubBaseUrl + ":" +
                applicationConfigHandler.ApplicationConfig.NetworkConfig.SignalRHubBaseUrlPort +
                applicationConfigHandler.ApplicationConfig.NetworkConfig.SignalRHubEndpoint;
            this.Initialize(url, async () =>
            {
                return applicationConfigHandler.ApplicationConfig.AccountConfig.UserSession.Token;
            },
            applicationConfigHandler.ApplicationConfig.NetworkConfig.SignalRTransportProtocol,
            applicationConfigHandler.ApplicationConfig.NetworkConfig.SignalRTransferFormat);

            _messageDataInterceptor = messageDataInterceptor;
            this.HubConnectionReconnectedEvent += new EventHandler<string>(SignalrClientReconnectedToBackendEvent);

        }

        public void SignalrClientReconnectedToBackendEvent(object sender,string args)
        {

        }

        public override void InitClientMethods()
        {

            HubConnection.On<List<MessageDTO>>(nameof(ReceiveMessage), ReceiveMessage);
            HubConnection.On<Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish.UserJellyfishDTO>(nameof(AcceptFriendshipRequest), AcceptFriendshipRequest);
            HubConnection.On<UserFriendshipRequestDTO>(nameof(ReceiveFriendshipRequest), ReceiveFriendshipRequest);
        }

        public Task AcceptFriendshipRequest(Shared.Kernel.Application.Model.DataTransferObject.ConcreteImplementation.Jellyfish.UserJellyfishDTO userDTO)
        {
            _messageDataInterceptor.ReceiveAcceptFriendRequest(userDTO);
            return Task.CompletedTask;
        }

        public Task ReceiveFriendshipRequest(UserFriendshipRequestDTO request)
        {
            _messageDataInterceptor.ReceiveFriendRequest(request);
            return Task.CompletedTask;
        }

        public Task ReceiveMessage(List<MessageDTO> messages)
        {
            _messageDataInterceptor.ReceiveMessage(messages.ToArray());
            return Task.CompletedTask;
        }
    }
}
