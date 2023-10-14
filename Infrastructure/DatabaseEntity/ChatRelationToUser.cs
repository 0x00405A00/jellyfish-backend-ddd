using Infrastructure.Abstractions;
using System;
using System.Collections.Generic;

namespace Infrastructure.DatabaseEntity;

public partial class ChatRelationToUser : DatabaseEntityModel
{
    public Guid ChatUuid { get; set; } = Guid.Empty;

    public Guid UserUuid { get; set; } = Guid.Empty;
    public sbyte? IsChatAdmin { get; set; } = 0;

    public virtual Chat ChatUu { get; set; } = null!;

    public virtual User UserUu { get; set; } = null!;
}
