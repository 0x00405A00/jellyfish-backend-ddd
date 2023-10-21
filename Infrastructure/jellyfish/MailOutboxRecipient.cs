using System;
using System.Collections.Generic;

namespace Infrastructure.jellyfish;

public partial class MailOutboxRecipient
{
    public string MailUuid { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string EmailTypeUuid { get; set; } = null!;

    public DateTime? CreatedTime { get; set; }

    public DateTime? DeletedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public virtual EmailType EmailTypeUu { get; set; } = null!;

    public virtual MailOutbox MailUu { get; set; } = null!;
}
