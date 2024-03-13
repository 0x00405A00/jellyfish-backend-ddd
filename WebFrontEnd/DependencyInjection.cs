using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using MudBlazor.Services;
using RestSharp;
using Shared.Const;
using Shared.Infrastructure.Backend;
using Shared.Infrastructure.Backend.Api;
using Shared.Infrastructure.Backend.SignalR;
using WebFrontEnd.Authentification;
using WebFrontEnd.BackgroundService;
using WebFrontEnd.Handler;
using WebFrontEnd.Service.Navigation;
using WebFrontEnd.Service.WebStorage;
using WebFrontEnd.Service.WebStorage.LocalStorage;

namespace WebFrontEnd
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceProvider)
        {
            serviceProvider.AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;

                config.SnackbarConfiguration.PreventDuplicates = true;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 2000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            });
            serviceProvider.AddSingleton<ISystemClock, SystemClock>();
            serviceProvider.AddScoped<IExtendedNavigationManager, ExtendedNavigationManager>();
            serviceProvider.AddScoped<ILocalStorageService, LocalStorageService>();
            serviceProvider.AddScoped<ISessionStorage, SessionStorage>();
            var sp = serviceProvider.BuildServiceProvider();
            var configuration = sp.GetService<IConfiguration>();


            // Add services to the container.
            serviceProvider.AddRazorPages(opt =>
            {
                string razorPagesRootDirectory = Path.Combine("/UI","");
                opt.RootDirectory = razorPagesRootDirectory;

            });
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
            var infrastructureApiSection = configuration.GetSection("Infrastructure:Api");
            var apiBaseUrl = infrastructureApiSection.GetValue<string>("BaseUrl");
            serviceProvider.AddScoped(sp => new RestClient(new Uri(apiBaseUrl)));
            serviceProvider.AddScoped<WebApiRestClient>();


            var infrastructureSignalRSection = configuration.GetSection("Infrastructure:SignalR");
            string baseUrl = infrastructureSignalRSection["SignalRHubBaseUrl"];
            int port = infrastructureSignalRSection.GetValue<int>("SignalRHubBaseUrlPort");
            string hub = infrastructureSignalRSection["SignalRHubEndpoint"];
            string transportProtocol = infrastructureSignalRSection["SignalRHubClientTransportProtocol"];
            serviceProvider.AddScoped<SignalRConnectionParams>(sp=>new SignalRConnectionParams(baseUrl,port,hub,transportProtocol));
            serviceProvider.AddScoped<SignalRClient>();

            serviceProvider.AddScoped<ICustomAuthentificationStateProvider,CustomAuthentificationStateProvider>();
            serviceProvider.AddScoped<AuthenticationStateProvider>(s => (CustomAuthentificationStateProvider)s.GetRequiredService<ICustomAuthentificationStateProvider>());

            serviceProvider.AddScoped<JellyfishBackendApi>();

            serviceProvider.AddScoped<DialogHandler>();

            serviceProvider.AddSingleton<LogoutBackgroundService>();
            serviceProvider.AddHostedService<LogoutBackgroundService>(p => p.GetService<LogoutBackgroundService>());
            return serviceProvider;
        }
    }
}
