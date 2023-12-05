using Microsoft.AspNetCore.StaticFiles;

namespace Shared.Authentification.Service
{
    public interface IFilePermissionService
    {
        void SetFilePermissions(StaticFileResponseContext context);
    }
}
