using System;
using System.Collections.Generic;

namespace Infrastructure.jellyfish;

public partial class Role
{
    public string Uuid { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime? DeletedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public DateTime? CreatedTime { get; set; }

    public sbyte? Deleted { get; set; }

    public virtual ICollection<UserRelationToRole> UserRelationToRoles { get; set; } = new List<UserRelationToRole>();
}
