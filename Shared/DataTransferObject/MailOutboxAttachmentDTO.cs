using System.Text.Json.Serialization;
using Domain.Entities.Mails;
using Shared.DataTransferObject.Abstraction;

namespace Shared.DataTransferObject
{
    public class MailOutboxAttachmentDTO : AbstractDTO, IMailOutboxAttachment
    {
        [JsonPropertyName("mime_mediatype")]
        public string MimeMediatype { get; set; } = null!;
        [JsonPropertyName("mime_mediasubtype")]
        public string MimeMediasubtype { get; set; } = null!;
        [JsonPropertyName("filename")]
        public string Filename { get; set; } = null!;

        [JsonPropertyName("attachment_sha1")]
        public string AttachmentSha1 { get; set; } = null!;
        [JsonPropertyName("is_embedded_in_html")]
        public bool? IsEmbeddedInHtml { get; set; } = null!;


    }
}
