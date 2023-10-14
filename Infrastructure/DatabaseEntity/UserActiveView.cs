using Infrastructure.Abstractions;
using System;
using System.Collections.Generic;

namespace Infrastructure.DatabaseEntity;

public partial class UserActiveView : DatabaseEntityModel
{
    public Guid Uuid { get; set; } = Guid.Empty;

    public Guid UserTypeUuid { get; set; } = Guid.Empty;

    public string User { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? UserProfilePicFileExt { get; set; }
}
