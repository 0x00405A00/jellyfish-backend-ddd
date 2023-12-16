using Domain.Entities.User;
using Domain.Primitives;

namespace Domain.Entities.Auth;

public sealed class Auth : Entity<AuthId>, IAuditibleCreateEntity
{
    public UserId UserId { get; private set; }
    public string? IpAddrRemote { get; private set; }
    public int? RemotePort { get; private set; }
    public string? IpAddrLocal { get; private set; }
    public int? LocalPort { get; private set; }
    public string Token { get; private set; } = null!;
    public DateTime TokenExpiresIn { get; private set; }
    public string UserAgent { get; private set; } = null!;
    public DateTime? LogoutDatetime { get; private set; }
    public string RefreshToken { get; private set; } = null!;
    public DateTime RefreshTokenExpiresIn { get; private set; }
    public User.User User { get; private set; } = null!;

    public DateTime? CreatedTime { get; private set; }
    public User.User? CreatedByUser { get; private set; }

    private Auth()
    {

    }

    public static Auth Create(
        AuthId authId,
        UserId userId,
        string ipAddrRemote,
        int? remotePort,
        string ipAddrLocal,
        int? localPort,
        string token,
        DateTime tokenExpiresIn,
        string userAgent,
        string refreshToken,
        DateTime refreshTokenExpiresIn,
        DateTime createTime,
        User.User user)
    {
        if (string.IsNullOrWhiteSpace(ipAddrRemote))
        {
            throw new ArgumentException("IP address remote cannot be null or empty.", nameof(ipAddrRemote));
        }

        if (string.IsNullOrWhiteSpace(ipAddrLocal))
        {
            throw new ArgumentException("IP address local cannot be null or empty.", nameof(ipAddrLocal));
        }

        if (string.IsNullOrWhiteSpace(token))
        {
            throw new ArgumentException("Token cannot be null or empty.", nameof(token));
        }

        if (string.IsNullOrWhiteSpace(userAgent))
        {
            throw new ArgumentException("User agent cannot be null or empty.", nameof(userAgent));
        }

        var auth = new Auth
        {
            CreatedTime = createTime,
            Id = authId,
            UserId = userId,
            IpAddrRemote = ipAddrRemote,
            RemotePort = remotePort,
            IpAddrLocal = ipAddrLocal,
            LocalPort = localPort,
            Token = token,
            TokenExpiresIn = tokenExpiresIn,
            UserAgent = userAgent,
            RefreshToken = refreshToken,
            RefreshTokenExpiresIn = refreshTokenExpiresIn,
            User = user
        };

        return auth;
    }

    public void Logout()
    {
        LogoutDatetime = DateTime.Now;
    }
    public void Refresh(TimeSpan texpMinTimeSpan,TimeSpan rexpMinTimeSpan)
    {
        TokenExpiresIn = DateTime.Now.Add(texpMinTimeSpan);
        RefreshTokenExpiresIn = DateTime.Now.Add(rexpMinTimeSpan);
    }

    public void SetCreated(User.User createdBy)
    {
        this.CreatedTime = DateTime.Now;
        this.CreatedByUser = createdBy;
    }

}
