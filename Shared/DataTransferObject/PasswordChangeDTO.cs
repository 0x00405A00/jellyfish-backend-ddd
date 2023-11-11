using System.Text.Json.Serialization;

namespace Shared.DataTransferObject
{
    public class PasswordChangeDTO : IDataTransferObject
    {
        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("password_repeat")]
        public string PasswordRepeat { get; set; }
    }
}
