using System;
using System.Collections.Generic;

namespace Infrastructure.jellyfish;

public partial class EmailType
{
    public string Uuid { get; set; } = null!;

    public string Type { get; set; } = null!;

    public DateTime? CreatedTime { get; set; }

    public DateTime? DeletedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public virtual ICollection<MailOutboxRecipient> MailOutboxRecipients { get; set; } = new List<MailOutboxRecipient>();
}
