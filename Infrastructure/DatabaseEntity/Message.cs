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

    //For future azure blob storage
    public string? BinaryContentPath { get; set; }
    public string? BinaryContentFileExt { get; set; }
    //For current mysql data store
    public string? BinaryContentBase64 { get; set; }
    public string? BinaryContentMimeType { get; set; }

    public virtual Chat ChatUu { get; set; } = null!;

    public virtual MessageAcknowledge? MessageAcknowledge { get; set; }

    public virtual User MessageOwnerNavigation { get; set; }
}
