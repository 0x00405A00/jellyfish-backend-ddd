using Microsoft.AspNetCore.Components.Authorization;
using RestSharp;
using Shared.Const;
using WebFrontEnd.Authentification;
using WebFrontEnd.BackgroundService;
using WebFrontEnd.Handler;
using WebFrontEnd.Service.Backend.Api;
using WebFrontEnd.Service.Backend.SignalR;
using WebFrontEnd.Service.WebStorage.LocalStorage;
using WebFrontEnd.Service.WebStorage;
using MudBlazor.Services;
using Microsoft.AspNetCore.Authentication;
using WebFrontEnd.Service.Navigation;

namespace WebFrontEnd
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceProvider)
        {

            serviceProvider.AddMudServices();
            serviceProvider.AddSingleton<ISystemClock, SystemClock>();
            serviceProvider.AddSingleton<IExtendedNavigationManager, ExtendedNavigationManager>();
            serviceProvider.AddScoped<ILocalStorageService, LocalStorageService>();
            serviceProvider.AddScoped<ISessionStorage, SessionStorage>();
            var sp = serviceProvider.BuildServiceProvider();
            var configuration = sp.GetService<IConfiguration>();

            var infrastructureApiSection = configuration.GetSection("Infrastructure:Api");
            var apiBaseUrl = infrastructureApiSection.GetValue<string>("BaseUrl");
            serviceProvider.AddScoped(sp => new RestClient(new Uri(apiBaseUrl)));
            serviceProvider.AddScoped<WebApiRestClient>();
            serviceProvider.AddScoped<SignalRClient>();

            // Add services to the container.
            serviceProvider.AddRazorPages();
            serviceProvider.AddServerSideBlazor();

            serviceProvider.AddOptions();
            serviceProvider.AddScoped<WebFrontEnd.Service.Authentification.IAuthentificationService, WebFrontEnd.Service.Authentification.AuthentificationService>();
            serviceProvider.AddAuthorizationCore(options =>
            {
                //User Policy: Any user with the role root
                options.AddPolicy(AuthorizationConst.Policy.RootPolicy, policy =>
                                  policy.RequireClaim(AuthorizationConst.Claims.ClaimTypeIsRoot, bool.TrueString));
                //User Policy: Any user with the role admin
                options.AddPolicy(AuthorizationConst.Policy.AdminPolicy, policy =>
                                  policy.RequireClaim(AuthorizationConst.Claims.ClaimTypeIsAdmin, bool.TrueString));
                //User Policy: Any registered User that confirms his registration
                options.AddPolicy(AuthorizationConst.Policy.UserPolicy, policy =>
                                  policy.RequireClaim(AuthorizationConst.Claims.ClaimTypeIsActivatedUser, bool.TrueString));
            });
            serviceProvider.AddScoped<CustomAuthentificationStateProvider>();
            serviceProvider.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomAuthentificationStateProvider>());

            serviceProvider.AddScoped<DialogHandler>();

            serviceProvider.AddSingleton<LogoutBackgroundService>();
            serviceProvider.AddHostedService<LogoutBackgroundService>(p => p.GetService<LogoutBackgroundService>());
            return serviceProvider;
        }
    }
}
