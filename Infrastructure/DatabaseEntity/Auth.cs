using Infrastructure.Abstractions;
using System;
using System.Collections.Generic;

namespace Infrastructure.DatabaseEntity;

public partial class Auth : DatabaseEntityModel
{
    public Guid Uuid { get; set; } = Guid.Empty;

    public Guid UserUuid { get; set; } = Guid.Empty;

    public string? IpAddrv4Remote { get; set; }

    public string? IpAddrv6Remote { get; set; }

    public int? RemotePort { get; set; }

    public string? IpAddrv4Local { get; set; }

    public string? IpAddrv6Local { get; set; }

    public int? LocalPort { get; set; }

    public string Token { get; set; } = null!;

    public DateTime TokenExpiresIn { get; set; }

    public string UserAgent { get; set; } = null!;

    public DateTime? LogoutDatetime { get; set; }

    public string RefreshToken { get; set; } = null!;

    public DateTime RefreshTokenExpiresIn { get; set; }

    public virtual User UserUu { get; set; } = null!;
}
