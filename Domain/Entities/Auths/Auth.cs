using Domain.Entities.Users;
using Domain.Extension;
using Domain.Primitives;
using Domain.Primitives.Ids;

namespace Domain.Entities.Auths;

public interface IAuth
{
    string LocalIp { get; }
    int LocalIpPort { get; }
    CustomDateTime? LogoutTime { get; }
    string RefreshToken { get; }
    CustomDateTime RefreshTokenExpiresIn { get; }
    string RemoteIp { get; }
    int RemoteIpPort { get; }
    string Token { get; }
    CustomDateTime TokenExpiresIn { get; }
    User User { get; set; }
    string UserAgent { get; }
    UserId UserId { get; }
}
public sealed partial class Auth : Entity<AuthId>, IAuth
{
    public UserId UserId { get; private set; }

    public string RemoteIp { get; private set; }
    public int RemoteIpPort { get; private set; }
    public string LocalIp { get; private set; }
    public int LocalIpPort { get; private set; }
    public string Token { get; private set; }
    public CustomDateTime TokenExpiresIn { get; private set; }
    public string UserAgent { get; private set; }
    public CustomDateTime RefreshTokenExpiresIn { get; private set; }
    public string RefreshToken { get; private set; }
    public CustomDateTime? LogoutTime { get; private set; }

    private Auth() : base()
    {

    }
    private Auth(
        AuthId id,
        UserId userId,
        string remoteIp,
        int remoteIpPort,
        string localIp,
        int localIpPort,
        string token,
        CustomDateTime tokenExpiresIn,
        string userAgent,
        CustomDateTime refreshTokenExpiresIn,
        string refreshToken,
        CustomDateTime? logoutTime) : base(id)
    {

        UserId = userId;
        RemoteIp = remoteIp;
        RemoteIpPort = remoteIpPort;
        LocalIp = localIp;
        LocalIpPort = localIpPort;
        Token = token;
        TokenExpiresIn = tokenExpiresIn;
        UserAgent = userAgent;
        RefreshTokenExpiresIn = refreshTokenExpiresIn;
        RefreshToken = refreshToken;
        LogoutTime = logoutTime;
    }
    public static Auth Create(
        AuthId id,
        UserId userId,
        string remoteIp,
        int remoteIpPort,
        string localIp,
        int localIpPort,
        string token,
        CustomDateTime tokenExpiresIn,
        string userAgent,
        CustomDateTime refreshTokenExpiresIn,
        string refreshToken,
        CustomDateTime? logoutTime)
    {
        if (string.IsNullOrWhiteSpace(remoteIp))
        {
            throw new ArgumentException("IP address remote cannot be null or empty.", nameof(remoteIp));
        }

        if (string.IsNullOrWhiteSpace(localIp))
        {
            throw new ArgumentException("IP address local cannot be null or empty.", nameof(localIp));
        }

        if (string.IsNullOrWhiteSpace(token))
        {
            throw new ArgumentException("Token cannot be null or empty.", nameof(token));
        }

        if (string.IsNullOrWhiteSpace(userAgent))
        {
            throw new ArgumentException("User agent cannot be null or empty.", nameof(userAgent));
        }

        return new Auth(
            id,
            userId,
            remoteIp,
            remoteIpPort,
            localIp,
            localIpPort,
            token,
            tokenExpiresIn,
            userAgent,
            refreshTokenExpiresIn,
            refreshToken,
            logoutTime);
    }

    public void Logout()
    {
        LogoutTime = DateTime.UtcNow.ToTypedDateTime();
    }
    public void Refresh(TimeSpan texpMinTimeSpan, TimeSpan rexpMinTimeSpan)
    {
        TokenExpiresIn = DateTime.UtcNow.Add(texpMinTimeSpan).ToTypedDateTime();
        RefreshTokenExpiresIn = DateTime.UtcNow.Add(rexpMinTimeSpan).ToTypedDateTime();
    }
}
public sealed partial class Auth
{
    public User User { get; set; }
}
