using Shared.ApiDataTransferObject;
using Shared.DataTransferObject;
using WebFrontEnd.Service.Backend.Api;

namespace WebFrontEnd.Service.Authentification
{
    public interface IAuthentificationService
    {
        Task<bool> Login(string userName,string password,CancellationToken cancellationToken);
        Task<bool> Logout(CancellationToken cancellationToken);
        Task<AuthDTO> GetCurrentAuthentification(CancellationToken cancellationToken);
        Task<WebApiHttpRequestResponseModel<ApiDataTransferObject<RegisterUserDTO>>> Register(RegisterUserDTO registerUserDTO,CancellationToken cancellationToken); 
    }
}
