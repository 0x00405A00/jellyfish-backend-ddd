using Infrastructure.Abstractions;
using System;
using System.Collections.Generic;

namespace Infrastructure.DatabaseEntity;

public partial class UserFriendshipRequest : DatabaseEntityModel
{
    public Guid UserUuid { get; set; } = Guid.Empty;

    public Guid TargetUserUuid { get; set; } = Guid.Empty;

    public string? TargetUserRequestMessage { get; set; }

    public virtual User TargetUserUu { get; set; } = null!;

    public virtual User UserUu { get; set; } = null!;
}
