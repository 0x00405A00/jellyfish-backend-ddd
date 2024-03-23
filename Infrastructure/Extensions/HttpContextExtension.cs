using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Shared.Const;
using System.Security.Claims;

namespace Infrastructure.Extensions
{
    public static class HttpContextExtension
    {
        public static List<Claim> GetClaims(this HttpContext httpContext, Func<Claim, bool> condition)
        {
            List<Claim> claim = new List<Claim>();
            if (httpContext != null)
            {
                if (httpContext.User != null)
                {
                    claim = Shared.Authentification.Claims.Extension.GetClaims(httpContext.User.Claims, condition);
                }
            }
            return claim;
        }
        public static Guid GetUserUuidFromRequest(this HttpContext context)
        {
            return ExtractUserUuidFromClaimsPrinciple(context.User);
        }
        public static Guid GetUserUuidFromRequest(this HubCallerContext context)
        {
            return ExtractUserUuidFromClaimsPrinciple(context.User);
        }
        private static Guid ExtractUserUuidFromClaimsPrinciple(ClaimsPrincipal claimsPrinciple)
        {
            string uuidStr = null;
            try
            {
                var userUuid = claimsPrinciple.Claims.FirstOrDefault(x => x.Type == AuthorizationConst.Claims.ClaimTypeUserUuid);
                uuidStr = userUuid != null ? userUuid.Value : string.Empty;
            }
            catch (Exception ex)
            {

            }
            return Guid.TryParse(uuidStr, out var uuid) ? uuid : Guid.Empty;
        }
        public static string GetAuthorizationHeader(this HttpContext context)
        {
            return context.Request.Headers.Authorization;
        }
    }
}
