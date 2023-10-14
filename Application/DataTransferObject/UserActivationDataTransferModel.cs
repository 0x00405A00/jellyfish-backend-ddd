using System.Text.Json.Serialization;

namespace Application.DataTransferObject
{
    public class UserActivationDataTransferModel
    {
        [JsonPropertyName("activation_code")]
        public string ActivationCode { get; set; }
    }
}
