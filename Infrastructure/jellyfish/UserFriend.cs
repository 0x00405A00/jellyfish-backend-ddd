using System;
using System.Collections.Generic;

namespace Infrastructure.jellyfish;

public partial class UserFriend
{
    public string Uuid { get; set; } = null!;

    public string UserUuid { get; set; } = null!;

    public string FriendUserUuid { get; set; } = null!;

    public DateTime? CreatedTime { get; set; }

    public sbyte? Deleted { get; set; }

    public DateTime? DeletedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public virtual User FriendUserUu { get; set; } = null!;

    public virtual User UserUu { get; set; } = null!;
}
