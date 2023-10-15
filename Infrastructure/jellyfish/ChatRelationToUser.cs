using System;
using System.Collections.Generic;

namespace Infrastructure.jellyfish;

public partial class ChatRelationToUser
{
    public string ChatUuid { get; set; } = null!;

    public string UserUuid { get; set; } = null!;

    public DateTime? CreatedTime { get; set; }

    public sbyte? Deleted { get; set; }

    public DateTime? DeletedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public sbyte? IsChatAdmin { get; set; }

    public virtual Chat ChatUu { get; set; } = null!;

    public virtual User UserUu { get; set; } = null!;
}
