using Application.DateTimeExtension;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Infrastructure.Authentification
{
    public class AuthentificationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {
        private readonly IOptionsMonitor<BasicAuthenticationOptions> _options;
        private readonly IConfiguration _configuration;

        public AuthentificationHandler(IOptionsMonitor<BasicAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock,IConfiguration configuration)
            : base(options, logger, encoder, clock)
        {
            _options = options;
            this._configuration = configuration;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var endpoint = Context.Features.Get<IEndpointFeature>()?.Endpoint;
            bool isSwaggerEndpoint = (Request.Path.HasValue && Request.Path.Value.ToLower().Contains("swagger"));
            if (endpoint == null && !isSwaggerEndpoint)//endpoint is null by requesting swagger doc endpoint or when route does not exists
            {
                return AuthenticateResult.NoResult();
            }
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                return AuthenticateResult.NoResult();
            }
             


            string token = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.Fail("unauthorized");
            }

            AuthenticateResult authenticateResult = AuthenticateResult.Fail("unauthorized");
            try
            {
                authenticateResult = await ValidateTokenAuthenticateResult(token);
                return authenticateResult;
            }
            catch (Exception ex)
            {
            }
            if (authenticateResult != null && !authenticateResult.Succeeded)
            {
                if (this.Context.WebSockets.IsWebSocketRequest)
                {
                    try
                    {
                        var webSocket = await this.Context.WebSockets.AcceptWebSocketAsync();
                        await webSocket.CloseAsync(System.Net.WebSockets.WebSocketCloseStatus.NormalClosure, "forbidden", CancellationToken.None);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return AuthenticateResult.Fail("unauthorized");
        }


        protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            return base.HandleForbiddenAsync(properties);
        }


        private async Task<AuthenticateResult> ValidateTokenAuthenticateResult(string token)
        {
            var config = _configuration.GetSection("Infrastructure:Authentification:Jwt");
            var symetricKey = config["SymetricKey"];
            var issuer = config["Issuer"];
            var audience = config["Audience"];
            var texpMin = int.Parse(config["TokenExpiresMinutes"]);
            var rexpMin = int.Parse(config["RefreshTokenExpiresMinutes"]);
            var jwtToken = JwtHandler.DecodeJwt(token, symetricKey,issuer, audience);
            if(jwtToken==null)
            {
                return AuthenticateResult.Fail("unauthorized");
            }
            if (DateTimeExtension.UnixTimeStampToDateTime((long)jwtToken.Payload.Exp) <= DateTime.Now)
            {
                return AuthenticateResult.Fail("unauthorized");
            }
            var claims = jwtToken.Payload.Claims.ToList();
            if (claims != null)
            {

            }
            var identity = new ClaimsIdentity(claims, Scheme.Name);

            var principal = new System.Security.Principal.GenericPrincipal(identity, null);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
