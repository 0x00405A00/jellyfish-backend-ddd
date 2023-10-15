using System;
using System.Collections.Generic;

namespace Infrastructure.jellyfish;

public partial class Auth
{
    public string Uuid { get; set; } = null!;

    public string UserUuid { get; set; } = null!;

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

    public DateTime? CreatedTime { get; set; }

    public sbyte? Deleted { get; set; }

    public DateTime? DeletedTime { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public virtual User UserUu { get; set; } = null!;
}
