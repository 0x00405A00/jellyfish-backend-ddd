using Infrastructure.Abstractions;
using System;
using System.Collections.Generic;

namespace Infrastructure.DatabaseEntity;

public partial class UserFriend : DatabaseEntityModel
{
    public Guid Uuid { get; set; } = Guid.Empty;

    public Guid UserUuid { get; set; } = Guid.Empty;

    public Guid FriendUserUuid { get; set; } = Guid.Empty;

    public virtual User FriendUserUu { get; set; } = null!;

    public virtual User UserUu { get; set; } = null!;
}
