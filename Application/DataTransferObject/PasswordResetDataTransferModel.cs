using System.Text.Json.Serialization;

namespace Application.DataTransferObject
{
    public class PasswordResetDataTransferModel 
    {
        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("password_reset_code")]
        public string PasswordResetCode { get; set; }


    }
}
