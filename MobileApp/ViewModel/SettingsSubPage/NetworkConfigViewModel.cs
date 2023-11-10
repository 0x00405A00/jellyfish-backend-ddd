using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MobileApp.Attribute;
using MobileApp.Data.AppConfig.Abstraction;
using MobileApp.Validation;
using MobileApp.ViewModel.SettingsSubPage;

namespace MobileApp.Data.AppConfig.ConcreteImplements
{
    public class NetworkConfigViewModel : AbstractConfigViewModel<NetworkConfig>
    {
        [PropertyUiDisplayText("WebApiHttpClientTransportProtocol", false)]
        public ValidatableObject<NetworkConfig.HTTP_TRANSPORT_PROTOCOLS> WebApiHttpClientTransportProtocol { get; set; } = new ValidatableObject<NetworkConfig.HTTP_TRANSPORT_PROTOCOLS>();

        [PropertyUiDisplayText("WebApiBaseUrl", false)]
        public ValidatableObject<string> WebApiBaseUrl { get; set; } = new ValidatableObject<string>();
        [PropertyUiDisplayText("WebApiBaseUrlPort", false)]
        public ValidatableObject<uint> WebApiBaseUrlPort { get; set; } = new ValidatableObject<uint>();
        [PropertyUiDisplayText("WebApiPath", false)]
        public ValidatableObject<string> WebApiPath { get; set; } = new ValidatableObject<string>();

        [PropertyUiDisplayText("SignalRHubClientTransportProtocol", false)]
        public ValidatableObject<NetworkConfig.HTTP_TRANSPORT_PROTOCOLS> SignalRHubClientTransportProtocol { get; set; } = new ValidatableObject<NetworkConfig.HTTP_TRANSPORT_PROTOCOLS>();

        [PropertyUiDisplayText("SignalRHubBaseUrl", false)]
        public ValidatableObject<string> SignalRHubBaseUrl { get; set; } = new ValidatableObject<string>();

        [PropertyUiDisplayText("SignalRHubEndoint", false)]
        public ValidatableObject<string> SignalRHubEndoint { get; set; } = new ValidatableObject<string>();
        [PropertyUiDisplayText("SignalRHubBaseUrlPort", false)]
        public ValidatableObject<uint> SignalRHubBaseUrlPort { get; set; } = new ValidatableObject<uint>();

        [PropertyUiDisplayText("SignalRTransferFormat", false)]
        public ValidatableObject<Microsoft.AspNetCore.Connections.TransferFormat> SignalRTransferFormat = new ValidatableObject<Microsoft.AspNetCore.Connections.TransferFormat>();

        [PropertyUiDisplayText("SignalRTransportProtocol", false)]
        public ValidatableObject<Microsoft.AspNetCore.Http.Connections.HttpTransportType> SignalRTransportProtocol = new ValidatableObject<Microsoft.AspNetCore.Http.Connections.HttpTransportType>() { Value = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets | Microsoft.AspNetCore.Http.Connections.HttpTransportType.LongPolling };

        public NetworkConfigViewModel(NetworkConfig config) : base(config)
        {
        }

        public override void MapConfigDataWithDisplayData()
        {
            WebApiBaseUrl.Value = Config.WebApiBaseUrl;
            WebApiBaseUrlPort.Value = Config.WebApiBaseUrlPort;
            WebApiHttpClientTransportProtocol.Value = Config.WebApiHttpClientTransportProtocol;
            WebApiPath.Value = Config.WebApiPath;

            SignalRHubClientTransportProtocol.Value = Config.SignalRHubClientTransportProtocol;
            SignalRHubBaseUrl.Value = Config.SignalRHubBaseUrl;
            SignalRHubBaseUrlPort.Value = Config.SignalRHubBaseUrlPort;
            SignalRHubEndoint.Value = Config.SignalRHubEndpoint;
            SignalRTransferFormat.Value = Config.SignalRTransferFormat;
            SignalRTransportProtocol.Value = Config.SignalRTransportProtocol;
        }

        public override void Safe()
        {

            Config.WebApiBaseUrl = WebApiBaseUrl.Value;
            Config.WebApiBaseUrlPort = WebApiBaseUrlPort.Value;
            Config.SignalRHubClientTransportProtocol = SignalRHubClientTransportProtocol.Value;
            Config.SignalRHubBaseUrl = SignalRHubBaseUrl.Value;
            Config.SignalRHubBaseUrlPort = SignalRHubBaseUrlPort.Value;
            Config.SignalRHubEndpoint = SignalRHubEndoint.Value;
            Config.SignalRTransferFormat = SignalRTransferFormat.Value;
            Config.SignalRTransportProtocol = SignalRTransportProtocol.Value;
        }

        public override void AddValidations()
        {
            WebApiBaseUrl.Validations.Add(new IsNotNullOrEmptyRule());
            SignalRHubBaseUrl.Validations.Add(new IsNotNullOrEmptyRule());
        }
    }
}
