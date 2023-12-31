using System.Text.Json.Serialization;
using Domain.Entities.Mails;
using Shared.DataTransferObject.Abstraction;

namespace Shared.DataTransferObject
{
    public class MailOutboxRecipientDTO : AbstractDTO, IMailOutboxRecipient
    {
        [JsonPropertyName("email")]    
        public string Email { get; set; } = null!;

        [JsonPropertyName("email_type")]
        public string EmailType { get; set; } = null!;
    }
}
