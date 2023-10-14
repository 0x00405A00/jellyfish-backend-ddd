using Infrastructure.Abstractions;
using System;
using System.Collections.Generic;

namespace Infrastructure.DatabaseEntity;

public partial class MessageAcknowledge : DatabaseEntityModel
{
    public Guid MessageUuid { get; set; } = Guid.Empty;

    public Guid? UserUuid { get; set; }

    public virtual Message MessageUu { get; set; } = null!;

    public virtual User? UserUu { get; set; }
}
