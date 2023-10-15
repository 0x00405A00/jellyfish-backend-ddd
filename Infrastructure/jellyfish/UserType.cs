using System;
using System.Collections.Generic;

namespace Infrastructure.jellyfish;

public partial class UserType
{
    public string Uuid { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime? CreatedTime { get; set; }

    public sbyte? Deleted { get; set; }

    public DateTime? DeletedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
