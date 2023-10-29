using System.Text.Json.Serialization;

namespace Shared.DataTransferObject
{
    public class PasswordResetConfirmationCodeDataTransferModel
    {
        [JsonPropertyName("password_reset_code")]
        public string PasswordResetCode { get; set; }
    }
}
