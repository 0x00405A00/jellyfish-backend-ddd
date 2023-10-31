using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Shared.Const;
using System.Security.Claims;
using System.Text.Encodings.Web;
using WebFrontEnd.Authentification;

internal class AuthentificationHandler : AuthenticationHandler<CustomAuthenticationSchemeOptions>
{
    public AuthentificationHandler(
        IOptionsMonitor<CustomAuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
        ClaimsIdentity claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaim(new Claim(AuthorizationConst.Claims.ClaimTypeIsAdmin,bool.TrueString));
        claimsPrincipal.AddIdentity(claimsIdentity);
        AuthenticationTicket authenticationTicket = new AuthenticationTicket(claimsPrincipal,"Base");
        return Task.FromResult(AuthenticateResult.Success(authenticationTicket));
    }
}