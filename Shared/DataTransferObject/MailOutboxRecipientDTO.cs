using System.Text.Json.Serialization;

namespace Shared.DataTransferObject
{
    public class MailOutboxRecipientDTO : IDataTransferObject
    {
        [JsonPropertyName("email")]    
        public string Email { get; set; } = null!;

        [JsonPropertyName("email_type")]
        public string EmailType { get; set; } = null!;
    }
}
