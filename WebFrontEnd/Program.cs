using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using RestSharp;
using WebFrontEnd.Service.Backend.Api;
using WebFrontEnd.Service.Backend.SignalR;
using WebFrontEnd.Service.Backend.SignalR.Abstraction;
using WebFrontEnd.Service.WebStorage.LocalStorage;
using MudBlazor.Services;

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

        var sp = builder.Services.BuildServiceProvider();
        var configuration = sp.GetService<IConfiguration>();

        var infrastructureApiSection = configuration.GetSection("Infrastructure:Api");
        var apiBaseUrl = infrastructureApiSection.GetValue<string>("BaseUrl");
        builder.Services.AddScoped(sp => new RestClient(new Uri(apiBaseUrl)));
        builder.Services.AddScoped<WebApiRestClient>();
        builder.Services.AddScoped<SignalRClient>();


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
