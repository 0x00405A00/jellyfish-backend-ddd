﻿using System.Text.Json.Serialization;

namespace Shared.DataTransferObject
{
    public class AuthDTO 
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("token_expires_in")]
        public DateTime? TokenExpiresIn { get; set; }

        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }

        [JsonPropertyName("refresh_token_expires_in")]
        public DateTime? RefreshTokenExpiresIn { get; set; }

        public bool IsTokenExpired => TokenExpiresIn != null&&TokenExpiresIn <= DateTime.Now;
        public bool IsRefreshTokenExpired => RefreshTokenExpiresIn!=null&&RefreshTokenExpiresIn <= DateTime.Now;
        public bool IsAuthentificated => TokenExpiresIn != null && TokenExpiresIn > DateTime.Now;
    }
}
