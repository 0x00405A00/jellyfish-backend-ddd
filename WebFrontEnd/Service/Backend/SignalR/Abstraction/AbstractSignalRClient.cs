using Microsoft.AspNetCore.SignalR.Client;
using System.Reflection;

namespace WebFrontEnd.Service.Backend.SignalR.Abstraction
{
    public abstract class AbstractSignalRClient : ISignalRClient, IDisposable
    {
        public static readonly string HubConnectionSendMethod = "InvokeAsync";
        private bool _isInit;
        private bool _isBuilded;
        public IHubConnectionBuilder ConnectionBuilder { get; private set; }
        public HubConnection HubConnection { get; private set; }
        public string SignalRHubUrl { get; private set; }
        public Func<Task<string>> AccessTokenProviderAction { get; private set; }
        public Microsoft.AspNetCore.Http.Connections.HttpTransportType TransportType { get; private set; }
        public Microsoft.AspNetCore.Connections.TransferFormat TransferFormat { get; private set; }
        public EventHandler<string> HubConnectionReconnectedEvent { get; set; }

        public bool IsInit
        {
            get
            {
                return _isInit;
            }
        }
        public bool IsBuilded
        {
            get
            {
                return _isBuilded;
            }
        }
        public AbstractSignalRClient()
        {

        }
        public abstract void InitClientMethods();
        public void Initialize(string url, Func<Task<string>> accessTokenProviderAction, Microsoft.AspNetCore.Http.Connections.HttpTransportType transportType, Microsoft.AspNetCore.Connections.TransferFormat transferFormat)
        {
            if (IsInit)
            {
                throw new InvalidOperationException("handler is already initialized");
            }
            SignalRHubUrl = url;
            TransportType = transportType;
            TransferFormat = transferFormat;
            AccessTokenProviderAction = accessTokenProviderAction;
            _isInit = true;

        }

        public void BuildConnection()
        {
            if (!IsInit)
            {
                throw new InvalidOperationException("please initialize the handler correctly via method: " + nameof(Initialize) + "");
            }
            if (IsBuilded)
            {
                throw new InvalidOperationException("handler is already builded");
            }
            var userAgent = GetUserAgent();
            ConnectionBuilder = new HubConnectionBuilder()
                .WithUrl(SignalRHubUrl, options =>
                {
                    options.SkipNegotiation = false;
                    options.Headers.Add("User-Agent", userAgent);
                    options.AccessTokenProvider = () => AccessTokenProviderAction();
                    options.Transports = TransportType;
                    options.DefaultTransferFormat = TransferFormat;

                })

                .AddJsonProtocol(options =>
                {

                    options.PayloadSerializerOptions.PropertyNamingPolicy = null;
                })
                .WithAutomaticReconnect(new TimeSpan[] { new TimeSpan(0, 0, 5) });
            HubConnection = ConnectionBuilder.Build();
            HubConnection.ServerTimeout = HubConnection.DefaultServerTimeout;
            HubConnection.HandshakeTimeout = new TimeSpan(0, 0, 5);
            HubConnection.KeepAliveInterval = HubConnection.DefaultKeepAliveInterval;
            HubConnection.Reconnected += HubConnection_Reconnected;
            HubConnection.Reconnecting += HubConnection_Reconnecting;
            InitClientMethods();
            _isBuilded = true;
        }

        private Task HubConnection_Reconnecting(Exception? arg)
        {

            return Task.CompletedTask;
        }

        private Task HubConnection_Reconnected(string? arg)
        {
            if (HubConnectionReconnectedEvent != null)
            {
                HubConnectionReconnectedEvent.Invoke(this, arg);
            }
            return Task.CompletedTask;
        }

        public string GetUserAgent()
        {

            if (!IsInit)
            {
                throw new InvalidOperationException("please initialize the handler correctly via method: " + nameof(Initialize) + " and " + nameof(BuildConnection) + "");
            }
            var currentAssembly = Assembly.GetExecutingAssembly();
            var hubAssembly = Assembly.GetAssembly(typeof(HubConnection));
            var libInfoHub = System.Diagnostics.FileVersionInfo.GetVersionInfo(hubAssembly.Location);
            var libInfoCurAss = System.Diagnostics.FileVersionInfo.GetVersionInfo(hubAssembly.Location);
            string userAgent = libInfoCurAss.ProductName;
            return userAgent;
        }

        public async void OpenConnection()
        {
            try
            {

                await HubConnection.ConnectWithRetryAsync(5000, CancellationToken.None);

            }
            catch (Exception ex)
            {

            }
            finally
            {


            }
        }
        public async void CloseConnection()
        {
            try
            {

                if (HubConnection != null && HubConnection.State == HubConnectionState.Connected)
                {
                    await HubConnection.StopAsync();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async void Send(string methodName, object[] args, CancellationToken cancellationToken)
        {
            if (!IsInit || !IsBuilded)
            {
                throw new InvalidOperationException("please initialize the handler correctly via method: " + nameof(Initialize) + " and " + nameof(BuildConnection) + "");
            }
            if (HubConnection == null || HubConnection.State != HubConnectionState.Connected)
            {
                throw new InvalidOperationException("please open a new connection before you use " + nameof(Send) + "");
            }

            try
            {
                var methods = HubConnection.GetType().GetMethods();
                var targetMethod = methods.FirstOrDefault(m => (m.GetParameters().Length - 2) == args.Length && m.Name == HubConnectionSendMethod);//m.GetParameters().Length-2 Subtraktion von 2, da MethodName und CancelationToken keine Methodparams der SignalR Function sind
                if (targetMethod == null)
                {
                    throw new InvalidOperationException("cant find a method of " + HubConnectionSendMethod + " with a signatur of " + args.Length + " params");
                }
                object[] methodParams = new object[args.Length + 2];
                methodParams[0] = methodName;
                int i = 1;//da Idx 0 == Methodname
                args.ToList().ForEach(x =>
                {
                    methodParams[i] = x;
                    i++;
                });
                methodParams[methodParams.Length - 1] = cancellationToken;

                targetMethod.Invoke(HubConnection, args);
            }
            catch (Exception ex)
            {

            }
        }

        public void Dispose()
        {
            if (!IsInit || !IsBuilded)
            {
                throw new InvalidOperationException("please initialize the handler correctly via method: " + nameof(Initialize) + " and " + nameof(BuildConnection) + "");
            }
            CloseConnection();
        }
    }
}
