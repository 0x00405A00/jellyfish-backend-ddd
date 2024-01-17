using Shared.DataTransferObject;
using Shared.Infrastructure.Backend.Api;

namespace Shared.Infrastructure.Backend
{
    public interface ICustomAuthentificationStateProvider
    {
        Task<AuthDTO> GetCurrentAuthentification(CancellationToken cancellationToken);
        Task<bool> Login(string userName, string password, CancellationToken cancellationToken);
        Task<bool> Logout(CancellationToken cancellationToken);
        Task<AuthDTO> RefreshLogin(string token, string refreshToken, CancellationToken cancellationToken);
        Task<JellyfishBackendApi.JellyfishBackendApiResponse<UserDTO>> Register(RegisterUserDTO registerUserDTO, CancellationToken cancellationToken);
    }
}