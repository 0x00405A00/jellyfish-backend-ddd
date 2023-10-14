using Infrastructure.Abstractions;

namespace Infrastructure.DatabaseEntity;

public partial class UserRelationToRole : DatabaseEntityModel
{
    public Guid UserUuid { get; set; } = Guid.Empty;

    public Guid RoleUuid { get; set; } = Guid.Empty;

    public virtual Role RoleUu { get; set; } = null!;

    public virtual User UserUu { get; set; } = null!;
}
