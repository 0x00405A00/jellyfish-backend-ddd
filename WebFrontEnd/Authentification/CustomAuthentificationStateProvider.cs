using Microsoft.AspNetCore.Components.Authorization;
using Shared.Const;
using Shared.DataTransferObject;
using System.Security.Claims;
using WebFrontEnd.Service.Authentification;
using Shared.Authentification.Jwt;

namespace WebFrontEnd.Authentification
{
    public class CustomAuthentificationStateProvider : AuthenticationStateProvider,IAuthentificationService
    {
        private readonly CancellationTokenSource tokenSource = new CancellationTokenSource();
        private readonly IAuthentificationService authentificationService;

        public CustomAuthentificationStateProvider(IAuthentificationService authentificationService)
        {
            this.authentificationService = authentificationService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var auth = await this.GetCurrentAuthentification(tokenSource.Token);
            var jwtDecoded = JwtHandler.DecodeJwt(auth.Token,null,null,null);
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
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return result;
        }

        public async Task<bool> Register(RegisterUserDTO registerUserDTO, CancellationToken cancellationToken)
        {
            var result = await authentificationService.Register(registerUserDTO,cancellationToken);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return result;
        }
    }
}
