using System.Text.Json.Serialization;

namespace Shared.DataTransferObject
{
    public class UserActivationDataTransferModel
    {
        [JsonPropertyName("activation_code")]
        public string ActivationCode { get; set; }
    }
}
