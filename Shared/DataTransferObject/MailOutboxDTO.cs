using System.Text.Json.Serialization;
using Shared.DataTransferObject.Abstraction;

namespace Shared.DataTransferObject
{
    public class MailOutboxDTO : IDataTransferObject
    {
        [JsonPropertyName("created_time")]
        public DateTime? CreatedTime { get; set; }
        [JsonPropertyName("last_modified_time")]
        public DateTime? LastModifiedTime { get; set; }
        [JsonPropertyName("deleted_time")]
        public DateTime? DeletedTime { get; set; }
        [JsonPropertyName("uuid")]
        public Guid Uuid { get; set; } = Guid.Empty;

        [JsonPropertyName("from")]
        public string From { get; set; } = null!;

        [JsonPropertyName("subject")]
        public string? Subject { get; set; }

        [JsonPropertyName("mail_outbox_attachments")]
        public ICollection<MailOutboxAttachmentDTO> MailOutboxAttachments { get; set; } = new List<MailOutboxAttachmentDTO>();

        [JsonPropertyName("mail_outbox_recipients")]
        public ICollection<MailOutboxRecipientDTO> MailOutboxRecipients { get; set; } = new List<MailOutboxRecipientDTO>();
        [JsonIgnore]
        public string RecipientsAggregate
        {
            get
            {
                if (MailOutboxRecipients != null && MailOutboxRecipients.Any())
                {
                    string str = null;
                    foreach (var recipient in MailOutboxRecipients.Where(x => x.EmailType == "To"))
                    {
                        str += recipient.Email + ";";
                    }
                }
                return null;
            }
        }

    }
}
