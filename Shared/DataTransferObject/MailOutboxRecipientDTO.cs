using System.Text.Json.Serialization;
using Domain.Entities.Mails;
using Domain.Primitives.Ids;
using Shared.DataTransferObject.Abstraction;

namespace Shared.DataTransferObject
{
    public class MailOutboxRecipientDTO : AbstractDTO, IMailOutboxRecipient
    {
        [JsonPropertyName("email")]    
        public string Email { get; set; } = null!;

        [JsonPropertyName("email_type")]
        public string EmailType { get; set; } = null!;

        public EmailTypeId EmailSendingTypeForeignKey{ get; }

        public MailOutbox Mail { get; set; }

        public MailOutboxId MailOutboxForeignKey{ get; }

        public EmailSendingType SendingType { get; set; }
    }
}
