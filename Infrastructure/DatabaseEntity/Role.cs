using Infrastructure.Abstractions;
using System;
using System.Collections.Generic;

namespace Infrastructure.DatabaseEntity;

public partial class Role : DatabaseEntityModel
{
    public Guid Uuid { get; set; } = Guid.Empty;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<UserRelationToRole> UserRelationToRoles { get; set; } = new List<UserRelationToRole>();
}
