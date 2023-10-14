using System.Text.Json.Serialization;

namespace Application.DataTransferObject
{
    public class PasswordResetConfirmationCodeDataTransferModel
    {
        [JsonPropertyName("password_reset_code")]
        public string PasswordResetCode { get; set; }
    }
}
