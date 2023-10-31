using Shared.DataTransferObject;
using WebFrontEnd.Const;
using WebFrontEnd.Service.Backend.Api;
using WebFrontEnd.Service.WebStorage.LocalStorage;

namespace WebFrontEnd.Service.Authentification
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly IConfiguration configuration;
        private readonly WebApiRestClient webApiRestClient;
        private readonly ILocalStorageService localStorageService;

        public AuthentificationService(
            IConfiguration configuration,
            WebApiRestClient webApiRestClient,
            ILocalStorageService localStorageService)
        {
            this.configuration = configuration;
            this.webApiRestClient = webApiRestClient;
            this.localStorageService = localStorageService;
        }

        public async Task<AuthDTO> GetCurrentAuthentification(CancellationToken cancellationToken)
        {
            AuthDTO auth = new AuthDTO();
            try
            {
                auth = await localStorageService.GetDeserializedJsonItemFromKey<AuthDTO>(WebAppAuthorizationConst.BrowserLocalStorageItemKey.Authorization);
            }
            catch (Exception ex)
            {

            }
            return auth;
        }

        public async Task<bool> Login(string userName, string password, CancellationToken cancellationToken)
        {
            var response = await webApiRestClient.Authentificate(userName, password, cancellationToken);
            if (response == null||response.Token == null)
            {
                return false;
            }
            await localStorageService.SetDeserializedJsonItemFromKey(WebAppAuthorizationConst.BrowserLocalStorageItemKey.Authorization, response);
            return true;
        }

        public async Task<bool> Logout(CancellationToken cancellationToken)
        {
            var infrastructureApiSection = configuration.GetSection("Infrastructure:Api");
            var logoutEndpoint = infrastructureApiSection.GetValue<string>("LogoutEndpoint");
            var token = await GetCurrentAuthentification(cancellationToken) ;
            if (token == null)
                return false;

            await localStorageService.RemoveItem(WebAppAuthorizationConst.BrowserLocalStorageItemKey.Authorization);
            var logoutResponse = await webApiRestClient.Request<object>(logoutEndpoint, RestSharp.Method.Post, cancellationToken, null, headers: new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("Authorization", token.Token) });
            if (!logoutResponse.IsSuccessStatusCode)
                return false;
            return true;
        }

        public async Task<bool> Register(RegisterUserDTO registerUserDTO, CancellationToken cancellationToken)
        {
            var response = await webApiRestClient.Register(registerUserDTO, cancellationToken);
            if(!response.IsSuccess)
            {
                return false;
            }
            return true;
        }
    }
}
