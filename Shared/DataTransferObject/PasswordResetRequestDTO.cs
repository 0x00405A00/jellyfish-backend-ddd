using System.Text.Json.Serialization;
using Shared.DataTransferObject.Abstraction;

namespace Shared.DataTransferObject
{
    public class PasswordResetRequestDTO : IDataTransferObject
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
