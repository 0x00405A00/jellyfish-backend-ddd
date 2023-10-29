using Infrastructure.Abstractions;
using System;
using System.Collections.Generic;

namespace Infrastructure.DatabaseEntity;

public partial class Auth : DatabaseEntityModel
{
    public Guid Uuid { get; set; } = Guid.Empty;

    public Guid UserUuid { get; set; } = Guid.Empty;

    public string? IpAddrRemote { get; set; }
    public int? RemotePort { get; set; }

    public string? IpAddrLocal { get; set; }

    public int? LocalPort { get; set; }

    public string Token { get; set; } = null!;

    public DateTime TokenExpiresIn { get; set; }

    public string UserAgent { get; set; } = null!;

    public DateTime? LogoutDatetime { get; set; }

    public string RefreshToken { get; set; } = null!;

    public DateTime RefreshTokenExpiresIn { get; set; }

    public virtual User UserUu { get; set; } = null!;
}
