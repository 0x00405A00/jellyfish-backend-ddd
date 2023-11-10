using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MobileApp.Attribute;
using MobileApp.Data.AppConfig.Abstraction;
using MobileApp.Validation;

namespace MobileApp.Data.AppConfig.ConcreteImplements
{
    public class NetworkConfig : AbstractApplicationConfig
    {
        public enum HTTP_TRANSPORT_PROTOCOLS : int
        {
            HTTPS,
            HTTP,

        }
        #region WebApi
        public string WebApiBaseUrl { get; set; }
        public uint WebApiBaseUrlPort { get; set; }
        public string WebApiPath { get; set; }
        public HTTP_TRANSPORT_PROTOCOLS WebApiHttpClientTransportProtocol { get; set; }

        #endregion
        #region SignalR
        public HTTP_TRANSPORT_PROTOCOLS SignalRHubClientTransportProtocol { get; set; }
        public string SignalRHubBaseUrl { get; set; }
        public string SignalRHubEndpoint { get; set; }
        public uint SignalRHubBaseUrlPort { get; set; }
        public Microsoft.AspNetCore.Connections.TransferFormat SignalRTransferFormat = Microsoft.AspNetCore.Connections.TransferFormat.Text;
        public Microsoft.AspNetCore.Http.Connections.HttpTransportType SignalRTransportProtocol = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
        #endregion
        public NetworkConfig() {

            SetDefaults();
        }

        public override void SetDefaults()
        {
            WebApiBaseUrl = "10.100.0.200";
            WebApiPath = "/jelly-api-1";
            WebApiBaseUrlPort =5030;
            WebApiHttpClientTransportProtocol = HTTP_TRANSPORT_PROTOCOLS.HTTP;
            SignalRHubClientTransportProtocol = HTTP_TRANSPORT_PROTOCOLS.HTTP;
            SignalRHubBaseUrl = "10.100.0.200";
            SignalRHubEndpoint = "/messenger";
            SignalRHubBaseUrlPort = 5030;
            //SignalRTransferFormat = Microsoft.AspNetCore.Connections.TransferFormat.Text;
            //SignalRTransportProtocol = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
            base.SetDefaults();
        }

    }
}
