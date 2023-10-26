using System;
using System.Collections.Generic;

namespace Infrastructure.jellyfish;

public partial class MailOutboxAttachment
{
    public string MailUuid { get; set; } = null!;

    public int Order { get; set; }

    public string? Filename { get; set; }

    public byte[] Attachment { get; set; } = null!;

    public string AttachmentSha1 { get; set; } = null!;

    public DateTime? CreatedTime { get; set; }

    public DateTime? DeletedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public string? MimeMediatype { get; set; }

    public string? MimeMediasubtype { get; set; }

    public string Uuid { get; set; } = null!;

    public sbyte? IsEmbeddedInHtml { get; set; }

    public string? MimeCid { get; set; }

    public virtual MailOutbox MailUu { get; set; } = null!;
}
