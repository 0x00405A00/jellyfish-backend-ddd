using Microsoft.IdentityModel.Tokens;
using Shared.Const;
using Shared.DataFilter.Infrastructure;
using Shared.DataFilter.Presentation;
using System.Text.Json;

namespace Presentation.Abstractions
{
    public static class ApiControllerExtensions
    {
        public static Guid GetUserUuidFromRequest(this HttpContext context)
        {
            string uuidStr = null;
            try
            {
                var userUuid = context.User.Claims.FirstOrDefault(x => x.Type == AuthorizationConst.Claims.ClaimTypeUserUuid);
                uuidStr = userUuid != null ? userUuid.Value :   string.Empty;
            }
            catch
            {

            }
            return Guid.TryParse(uuidStr, out var uuid)?uuid:Guid.Empty;
        }

    }
}
