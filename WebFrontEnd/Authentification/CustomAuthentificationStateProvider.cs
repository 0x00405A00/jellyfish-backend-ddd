using Microsoft.AspNetCore.Components.Authorization;
using Shared.Authentification.Jwt;
using Shared.DataTransferObject;
using System.Security.Claims;
using WebFrontEnd.Service.Authentification;
using static WebFrontEnd.Service.Backend.Api.JellyfishBackendApi;

namespace WebFrontEnd.Authentification
{
    public class CustomAuthentificationStateProvider : AuthenticationStateProvider,IAuthentificationService,IDisposable
    {
        private readonly CancellationTokenSource tokenSource = new CancellationTokenSource();
        private readonly IAuthentificationService authentificationService;
        private bool disposedValue;

        public CustomAuthentificationStateProvider(IAuthentificationService authentificationService)
        {
            this.authentificationService = authentificationService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var auth = await this.GetCurrentAuthentification(tokenSource.Token);
            if(auth == null)
            {
                return new AuthenticationState(new ClaimsPrincipal());
            }
            if(auth.IsTokenExpired)
            {
                return new AuthenticationState(new ClaimsPrincipal());
            }
            var jwtDecoded = JwtHandler.DecodeJwt(auth.Token,null,null,null);
            if (jwtDecoded == null) 
            {
                return new AuthenticationState(new ClaimsPrincipal());
            }
            var identity = new ClaimsIdentity();
            try
            {
                var claims = jwtDecoded.Claims.ToList();
                identity = new ClaimsIdentity(claims);
            }
            catch (HttpRequestException ex)
            {
                throw;
            }
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public async Task<bool> Login(string userName, string password, CancellationToken cancellationToken)
        {
            var result = await authentificationService.Login(userName, password, cancellationToken);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return result;
        }

        public async Task<bool> Logout(CancellationToken cancellationToken)
        {
            var result = await authentificationService.Logout(cancellationToken);   
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return result;
        }

        public async Task<AuthDTO> GetCurrentAuthentification(CancellationToken cancellationToken)
        {
            var result = await authentificationService.GetCurrentAuthentification(cancellationToken);
            return result??new AuthDTO();
        }

        public async Task<JellyfishBackendApiResponse<UserDTO>> Register(RegisterUserDTO registerUserDTO, CancellationToken cancellationToken)
        {
            var result = await authentificationService.Register(registerUserDTO,cancellationToken);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: Verwalteten Zustand (verwaltete Objekte) bereinigen
                }

                // TODO: Nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalizer überschreiben
                // TODO: Große Felder auf NULL setzen
                disposedValue = true;
            }
        }

        // // TODO: Finalizer nur überschreiben, wenn "Dispose(bool disposing)" Code für die Freigabe nicht verwalteter Ressourcen enthält
         ~CustomAuthentificationStateProvider()
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

        public Task<AuthDTO> RefreshLogin(string token, string refreshToken, CancellationToken cancellationToken)
        {
            return authentificationService.RefreshLogin(token, refreshToken, cancellationToken);
        }

    }
}
