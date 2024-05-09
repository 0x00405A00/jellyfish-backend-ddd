using Domain.Entities.Auths;
using Domain.Entities.Users;
using Domain.Primitives;
using Domain.Primitives.Ids;
using Shared.DataTransferObject.Abstraction;
using System.Text.Json.Serialization;

namespace Shared.DataTransferObject
{
    public class AuthDTO :AbstractDTO//, IAuth
    {
        [JsonIgnore]
        public new Guid Id { get; set; }

        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("token_expires_in")]
        public DateTime? TokenExpiresIn { get; set; }

        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }

        [JsonPropertyName("refresh_token_expires_in")]
        public DateTime? RefreshTokenExpiresIn { get; set; }

        [JsonIgnore]
        public bool IsTokenExpired => TokenExpiresIn == null||TokenExpiresIn != null&&TokenExpiresIn <= DateTime.UtcNow;

        [JsonIgnore] 
        public bool IsRefreshTokenExpired => RefreshTokenExpiresIn==null||RefreshTokenExpiresIn != null&&RefreshTokenExpiresIn <= DateTime.UtcNow;

        [JsonIgnore] 
        public bool IsAuthentificated => !IsTokenExpired;

        [JsonIgnore]
        public string LocalIp{ get; }

        [JsonIgnore]
        public int LocalIpPort{ get; }

        [JsonIgnore]
        public CustomDateTime? LogoutTime{ get; }

        [JsonIgnore]
        public string RemoteIp{ get; }

        [JsonIgnore]
        public int RemoteIpPort{ get; }

        [JsonIgnore]
        public User User { get; set; }

        [JsonIgnore]
        public string UserAgent{ get; }

        [JsonIgnore]
        public UserId UserId{ get; }

        //CustomDateTime IAuth.RefreshTokenExpiresIn{ get; }

        //CustomDateTime IAuth.TokenExpiresIn{ get; }

        public List<KeyValuePair<string, string>> CreateAuthorizationHeader()
        {
            return new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("Authorization", Token) };
        }

        public override string ToString()
        {
            return string.Format("Token: {0}, Token-ExpiresIn: {1}, Refresh-Token: {2}, Refresh-Token-ExpiresIn: {3}",Token,TokenExpiresIn.ToString(),RefreshToken,RefreshTokenExpiresIn.ToString());
        }
    }
}
