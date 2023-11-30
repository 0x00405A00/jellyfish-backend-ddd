using System.Text.Json.Serialization;
using Shared.DataTransferObject.Abstraction;

namespace Shared.DataTransferObject
{
    public class UserActivationDataTransferModel : IDataTransferObject
    {
        [JsonPropertyName("activation_code")]
        public string ActivationCode { get; set; }
    }
}
