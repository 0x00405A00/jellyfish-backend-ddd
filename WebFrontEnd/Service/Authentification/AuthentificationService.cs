using Shared.DataTransferObject;
using WebFrontEnd.Const;
using WebFrontEnd.Service.Backend.Api;
using WebFrontEnd.Service.WebStorage.LocalStorage;

namespace WebFrontEnd.Service.Authentification
{
    public class AuthentificationService : IAuthentificationService,IDisposable
    {
        private readonly IConfiguration configuration;
        private readonly WebApiRestClient webApiRestClient;
        private readonly ILocalStorageService localStorageService;
        private bool disposedValue;

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
                auth = await localStorageService.GetDeserializedJsonItemFromKey<AuthDTO>(WebAppConst.BrowserLocalStorageItemKey.Authorization);
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
            await localStorageService.SetDeserializedJsonItemFromKey(WebAppConst.BrowserLocalStorageItemKey.Authorization, response);
            return true;
        }

        public async Task<bool> Logout(CancellationToken cancellationToken)
        {
            var infrastructureApiSection = configuration.GetSection("Infrastructure:Api");
            var logoutEndpoint = infrastructureApiSection.GetValue<string>("LogoutEndpoint");
            var token = await GetCurrentAuthentification(cancellationToken) ;
            if (token == null)
                return false;

            await localStorageService.RemoveItem(WebAppConst.BrowserLocalStorageItemKey.Authorization);
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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {

                }

                // TODO: Nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalizer überschreiben
                // TODO: Große Felder auf NULL setzen
                disposedValue = true;
            }
        }

        // // TODO: Finalizer nur überschreiben, wenn "Dispose(bool disposing)" Code für die Freigabe nicht verwalteter Ressourcen enthält
         ~AuthentificationService()
         {
             // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in der Methode "Dispose(bool disposing)" ein.
             Dispose(disposing: false);
         }

        public void Dispose()
        {
            // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in der Methode "Dispose(bool disposing)" ein.
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
