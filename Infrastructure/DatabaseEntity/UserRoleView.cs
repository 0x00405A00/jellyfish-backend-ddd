using Infrastructure.Abstractions;
using System;
using System.Collections.Generic;

namespace Infrastructure.DatabaseEntity;

public partial class UserRoleView : DatabaseEntityModel
{
    public Guid Uuid { get; set; } = Guid.Empty;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public Guid UserUuid { get; set; } = Guid.Empty;
}
