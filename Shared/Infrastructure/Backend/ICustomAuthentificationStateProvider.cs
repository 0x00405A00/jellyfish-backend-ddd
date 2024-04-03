using Microsoft.AspNetCore.Components.Authorization;
using Shared.DataTransferObject;
using Shared.Infrastructure.Backend.Api;
using System.Runtime.CompilerServices;

namespace Shared.Infrastructure.Backend
{
    public interface ICustomAuthentificationStateProvider
    {
        Task<AuthenticationState> GetAuthenticationStateAsync();
        Task<UserDTO> GetCurrentUser(CancellationToken cancellationToken);
        Task<AuthDTO> GetCurrentAuthentification(CancellationToken cancellationToken, [CallerMemberName] object caller = null);
        Task<bool> Login(string userName, string password, CancellationToken cancellationToken);
        Task<bool> Logout(CancellationToken cancellationToken);
        Task<AuthDTO> RefreshLogin(string token, string refreshToken, CancellationToken cancellationToken);
        Task<JellyfishBackendApi.JellyfishBackendApiResponse<UserDTO>> Register(RegisterUserDTO registerUserDTO, CancellationToken cancellationToken);
    }
}