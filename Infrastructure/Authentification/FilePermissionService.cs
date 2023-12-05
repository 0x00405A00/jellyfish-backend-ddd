using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Shared.Authentification.Service;

namespace Infrastructure.Authentification
{
    public class FilePermissionService : IFilePermissionService
    {

        public FilePermissionService()
        {
        }

        public void SetFilePermissions(StaticFileResponseContext context)
        {
            // Hier können Sie benutzerdefinierte Überprüfungen durchführen, z.B. auf Rollen basierend
            context.Context.Response.Headers.Add("Cache-Control", "no-store");

        }
    }
}
