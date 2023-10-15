using System;
using System.Collections.Generic;

namespace Infrastructure.jellyfish;

public partial class UserFriendshipRequest
{
    public string UserUuid { get; set; } = null!;

    public string TargetUserUuid { get; set; } = null!;

    public string? TargetUserRequestMessage { get; set; }

    public DateTime? CreatedTime { get; set; }

    public sbyte? Deleted { get; set; }

    public DateTime? DeletedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public virtual User TargetUserUu { get; set; } = null!;

    public virtual User UserUu { get; set; } = null!;
}
