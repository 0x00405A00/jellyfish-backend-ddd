using Shared.DataTransferObject;

namespace WebFrontEnd.Service.Authentification
{
    public interface IAuthentificationService
    {
        Task<bool> Login(string userName,string password,CancellationToken cancellationToken);
        Task<bool> Logout(CancellationToken cancellationToken);
        Task<AuthDTO> GetCurrentAuthentification(CancellationToken cancellationToken);
        Task<bool> Register(RegisterUserDTO registerUserDTO,CancellationToken cancellationToken); 
    }
}
