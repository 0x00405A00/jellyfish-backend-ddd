using System;
using System.Collections.Generic;

namespace Infrastructure.jellyfish;

public partial class MessageAcknowledge
{
    public string MessageUuid { get; set; } = null!;

    public string UserUuid { get; set; } = null!;

    public DateTime? CreatedTime { get; set; }

    public sbyte? Deleted { get; set; }

    public DateTime? DeletedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public virtual Message MessageUu { get; set; } = null!;

    public virtual User UserUu { get; set; } = null!;
}
