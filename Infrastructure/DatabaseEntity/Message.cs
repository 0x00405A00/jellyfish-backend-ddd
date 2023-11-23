using Infrastructure.Abstractions;
using System;
using System.Collections.Generic;

namespace Infrastructure.DatabaseEntity;

public partial class Message : DatabaseEntityModel
{
    public Guid Uuid { get; set; } = Guid.Empty;

    public Guid ChatUuid { get; set; } = Guid.Empty;

    public Guid? MessageOwner { get; set; }

    public string Text { get; set; } = null!;

    public string? BinaryContentPath { get; set; }
    public string? BinaryContentFileExt { get; set; }

    public virtual Chat ChatUu { get; set; } = null!;

    public virtual MessageAcknowledge? MessageAcknowledge { get; set; }

    public virtual User MessageOwnerNavigation { get; set; }
}
