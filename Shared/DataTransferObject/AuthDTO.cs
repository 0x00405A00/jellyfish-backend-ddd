using Domain.Entities.Auths;
using Domain.Entities.Users;
using Domain.Primitives;
using Domain.Primitives.Ids;
using Shared.DataTransferObject.Abstraction;
using System.Text.Json.Serialization;

namespace Shared.DataTransferObject
{
    public class AuthDTO :AbstractDTO, IAuth
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("token_expires_in")]
        public DateTime? TokenExpiresIn { get; set; }

        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }

        [JsonPropertyName("refresh_token_expires_in")]
        public DateTime? RefreshTokenExpiresIn { get; set; }

        public bool IsTokenExpired => TokenExpiresIn != null&&TokenExpiresIn <= DateTime.UtcNow;
        public bool IsRefreshTokenExpired => RefreshTokenExpiresIn!=null&&RefreshTokenExpiresIn <= DateTime.UtcNow;
        public bool IsAuthentificated => TokenExpiresIn != null && TokenExpiresIn > DateTime.UtcNow;


        public string LocalIp{ get; }

        public int LocalIpPort{ get; }

        public CustomDateTime? LogoutTime{ get; }

        public string RemoteIp{ get; }

        public int RemoteIpPort{ get; }

        public User User { get; set; }

        public string UserAgent{ get; }

        public UserId UserId{ get; }

        CustomDateTime IAuth.RefreshTokenExpiresIn{ get; }

        CustomDateTime IAuth.TokenExpiresIn{ get; }

        public List<KeyValuePair<string, string>> CreateAuthorizationHeader()
        {
            return new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("Authorization", Token) };
        }
}
}
