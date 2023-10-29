using System.Text.Json.Serialization;

namespace Shared.DataTransferObject
{
    public class PasswordResetDataTransferModel
    {
        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("password_repeat")]
        public string PasswordRepeat { get; set; }

        [JsonPropertyName("password_reset_code")]
        public string PasswordResetCode { get; set; }
        [JsonIgnore]
        public string? Base64Token { get; set; }


    }
}
