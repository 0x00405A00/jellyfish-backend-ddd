using Infrastructure.Abstractions;

namespace Infrastructure.DatabaseEntity;

public partial class MailOutboxAttachment : DatabaseEntityModel
{
    public Guid MailUuid { get; set; } = Guid.Empty;
    public Guid Uuid { get; set; } = Guid.Empty;

    public string MimeMediatype { get; set; } = null!;
    public string MimeMediasubtype { get; set; } = null!;
    public string Filename { get; set; } = null!;
    public string MimeCid { get; set; } = null!;
    public int Order { get; set; }
    public byte[] Attachment { get; set; } = null!;

    public string AttachmentSha1 { get; set; } = null!;
    public sbyte? IsEmbeddedInHtml { get; set; } = null!;

    public virtual MailOutbox MailUu { get; set; } = null!;
}
