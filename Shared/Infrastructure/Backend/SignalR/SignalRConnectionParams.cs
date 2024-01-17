namespace Shared.Infrastructure.Backend.SignalR
{
    public class SignalRConnectionParams(string baseUrl, int port, string hub, string transportProtocol)
    {
        public string BaseUrl { get; } = baseUrl;
        public int Port { get; } = port;
        public string Hub { get; } = hub;
        public string TransportProtocol { get; } = transportProtocol;

        public string GetConnectionUrl()
        {
            string baseUrl = BaseUrl + ":" +
                Port +
                Hub;
            return baseUrl;
        }
    }
}
