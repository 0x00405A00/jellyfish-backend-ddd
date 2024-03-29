﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shared.Authentification.Jwt
{
    public static class JwtHandler
    {
        public static JwtSecurityToken EncodeJwt(string symetricPrivateKey, string issuer, string audience, Claim[] claims, TimeSpan expires)
        {
            byte[] key = Encoding.UTF8.GetBytes(symetricPrivateKey);
            var symmetricSecurityKey = new SymmetricSecurityKey(key);
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.UtcNow.Add(expires),
                signingCredentials: credentials
            );

            return token;
        }
        public static JwtSecurityToken DecodeJwt(string token, string symetricPrivateKey, string issuer, string audience)
        {
            if(String.IsNullOrEmpty(token))
            {
                return null;
            }
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            if(symetricPrivateKey!=null)
            {

                byte[] key = Encoding.UTF8.GetBytes(symetricPrivateKey);
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                try
                {
                    SecurityToken validatedToken;
                    ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                    // 'validatedToken' enthält Informationen über das validierte Token.
                    // 'claimsPrincipal' enthält die Ansprüche (Claims) aus dem Token.

                    if (validatedToken is JwtSecurityToken jwtSecurityToken)
                    {
                        return validatedToken as JwtSecurityToken;
                    }
                    return null;
                }
                catch (SecurityTokenException ex)
                {
                    return null;
                }
            }
            else
            {
                return tokenHandler.ReadJwtToken(token);
            }
        }
    }
}
