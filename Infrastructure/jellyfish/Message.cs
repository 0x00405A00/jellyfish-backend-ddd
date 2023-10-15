using System;
using System.Collections.Generic;

namespace Infrastructure.jellyfish;

public partial class Message
{
    public string Uuid { get; set; } = null!;

    public string ChatUuid { get; set; } = null!;

    public string? MessageOwner { get; set; }

    public string Text { get; set; } = null!;

    public byte[]? BinaryContent { get; set; }

    public DateTime? CreatedTime { get; set; }

    public sbyte? Deleted { get; set; }

    public DateTime? DeletedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public virtual Chat ChatUu { get; set; } = null!;

    public virtual ICollection<MessageAcknowledge> MessageAcknowledges { get; set; } = new List<MessageAcknowledge>();

    public virtual User? MessageOwnerNavigation { get; set; }
}
