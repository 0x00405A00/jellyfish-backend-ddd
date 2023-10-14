using System.Text.Json.Serialization;

namespace Application.DataTransferObject
{
    public class PasswordResetRequestDTO
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }   
    }
}
