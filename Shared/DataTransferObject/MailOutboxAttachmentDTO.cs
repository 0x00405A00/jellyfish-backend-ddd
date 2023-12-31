using System.Text.Json.Serialization;
using Domain.Entities.Mails;
using Domain.Primitives.Ids;
using Shared.DataTransferObject.Abstraction;

namespace Shared.DataTransferObject
{
    public class MailOutboxAttachmentDTO : AbstractDTO, IMailOutboxAttachment
    {
        [JsonPropertyName("mime_mediatype")]
        public string MimeMediatype { get; set; } = null!;

        public string MimeMediaType{ get; }

        [JsonPropertyName("mime_mediasubtype")]
        public string MimeMediasubtype { get; set; } = null!;

        public string MimeMediaSubType{ get; }

        [JsonPropertyName("filename")]
        public string Filename { get; set; } = null!;

        [JsonPropertyName("attachment_sha1")]
        public string AttachmentSha1 { get; set; } = null!;
        [JsonPropertyName("is_embedded_in_html")]
        public bool? IsEmbeddedInHtml { get; set; } = null!;


        public string AttachmentPath{ get; }

        public bool? IsEmbededInHtml{ get; }

        public MailOutbox Mail { get; set; }

        public MailOutboxId MailOutboxForeignKey{ get; }

        public string MimeCid{ get; }

        public int Order{ get; }
    }
}
