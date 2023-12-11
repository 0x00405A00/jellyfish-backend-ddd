using Infrastructure.Abstractions;

namespace Infrastructure.DatabaseEntity;

public partial class ChatRelationToUser : DatabaseEntityModel
{
    public Guid Uuid { get; set; } = Guid.Empty;
    public Guid ChatUuid { get; set; } = Guid.Empty;

    public Guid UserUuid { get; set; } = Guid.Empty;
    public sbyte? IsChatAdmin { get; set; }

    public virtual Chat ChatUu { get; set; } = null!;

    public virtual User UserUu { get; set; } = null!;
}
