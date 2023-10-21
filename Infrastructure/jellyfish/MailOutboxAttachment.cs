using System;
using System.Collections.Generic;

namespace Infrastructure.jellyfish;

public partial class MailOutboxAttachment
{
    public string MailUuid { get; set; } = null!;

    public int Order { get; set; }
    public string Filename { get; set; } = null!;

    public byte[] Attachment { get; set; } = null!;

    public string AttachmentSha1 { get; set; } = null!;

    public DateTime? CreatedTime { get; set; }

    public DateTime? DeletedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public virtual MailOutbox MailUu { get; set; } = null!;
}
