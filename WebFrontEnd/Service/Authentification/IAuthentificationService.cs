using Microsoft.AspNetCore.Components.Authorization;
using Shared.DataTransferObject;
using static Shared.Infrastructure.Backend.Api.JellyfishBackendApi;

namespace WebFrontEnd.Service.Authentification
{
    public interface IAuthentificationService
    {
        Task<UserDTO> GetCurrentUser(AuthenticationState authenticationState,CancellationToken cancellationToken);
        Task<bool> Login(string userName,string password,CancellationToken cancellationToken);
        Task<bool> Logout(CancellationToken cancellationToken);
        Task<AuthDTO> GetCurrentAuthentification(CancellationToken cancellationToken);
        Task<AuthDTO> RefreshLogin(string token,string refreshToken,CancellationToken cancellationToken);
        Task<JellyfishBackendApiResponse<UserDTO>> Register(RegisterUserDTO registerUserDTO, CancellationToken cancellationToken); 
    }
}
