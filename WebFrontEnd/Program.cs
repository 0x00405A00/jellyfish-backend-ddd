using MudBlazor.Services;
using RestSharp;
using Shared.Const;
using WebFrontEnd.BackgroundService;
using WebFrontEnd.Service.Backend.Api;
using WebFrontEnd.Service.Backend.SignalR;
using WebFrontEnd.Service.WebStorage.LocalStorage;

public class Program
{
    public static async Task Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddMudServices();
        builder.Services.AddScoped<ILocalStorageService,LocalStorageService>();
        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddAuthorization(options =>
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

        var sp = builder.Services.BuildServiceProvider();
        var configuration = sp.GetService<IConfiguration>();

        var infrastructureApiSection = configuration.GetSection("Infrastructure:Api");
        var apiBaseUrl = infrastructureApiSection.GetValue<string>("BaseUrl");
        builder.Services.AddScoped(sp => new RestClient(new Uri(apiBaseUrl)));
        builder.Services.AddScoped<WebApiRestClient>();
        builder.Services.AddScoped<SignalRClient>();

        builder.Services.AddSingleton<LogoutBackgroundService>();
        builder.Services.AddHostedService< LogoutBackgroundService>(p=>p.GetService<LogoutBackgroundService>());

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}
