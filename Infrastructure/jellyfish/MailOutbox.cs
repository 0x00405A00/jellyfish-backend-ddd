using System;
using System.Collections.Generic;

namespace Infrastructure.jellyfish;

public partial class MailOutbox
{
    public string Uuid { get; set; } = null!;

    public string From { get; set; } = null!;

    public string? Subject { get; set; }

    public byte[]? Body { get; set; }

    public DateTime? CreatedTime { get; set; }

    public DateTime? DeletedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public sbyte? BodyIsHtml { get; set; }

    public virtual ICollection<MailOutboxAttachment> MailOutboxAttachments { get; set; } = new List<MailOutboxAttachment>();

    public virtual ICollection<MailOutboxRecipient> MailOutboxRecipients { get; set; } = new List<MailOutboxRecipient>();
}
