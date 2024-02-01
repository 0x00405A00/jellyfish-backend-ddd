using Application;
using Infrastructure;
using Infrastructure.Extension.WebAppBuilderExtensions;
using Infrastructure.Healthcheck.Response;
using Infrastructure.SignalR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.FileProviders;
using Presentation;
using Presentation.Middleware;
using Shared.Authentification.Service;
using Shared.Const;

namespace WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public struct ConfigurationFilesKeys
        {
            public const string GeneralConfig = "appsettings.json";
            public const string MailConfig = "appsettings-mail.json";
        }

        public Startup(
            IConfiguration configuration,
            IWebHostEnvironment env)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(ConfigurationFilesKeys.GeneralConfig, optional: false, reloadOnChange: true)
                .AddJsonFile(ConfigurationFilesKeys.MailConfig, optional: true, reloadOnChange: true);

            Configuration = config.AddConfiguration(configuration).Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddInfrastructure();
            services.AddApplication(new System.Reflection.Assembly[] { Application.AssemblyReference.Assembly });
            services.AddPresentation();
        }

        public void Configure(
            IApplicationBuilder app,
            IServiceProvider serviceProvider,
            IWebHostEnvironment env)
        {
            
            app.UseRouting();
            app.UseOverrideDefaultAspNetResponseMiddleware();
            app.UseAuthentication();
            app.UseAuthorization();
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseExceptionHandler("/error-debug");
                app.AppendMigrations();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            //Default wwwroot
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
                RequestPath = "/wwwroot",
                OnPrepareResponse = serviceProvider.GetRequiredService<IFilePermissionService>().SetFilePermissions
            }); // This line enables the serving of static files from the wwwroot folder.

            app.UseEndpoints(x =>
            {
                x.MapHealthChecks("/healthz", new HealthCheckOptions
                {
                    ResultStatusCodes =
                    {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                    },
                    ResponseWriter = HealthCheckResponseWriter.WriteResponse
                }).RequireAuthorization(AuthorizationConst.Policy.AdminPolicy);

                x.MapControllers();
                x.MapHub<MessengerHub>("/messengerhub");
            });
        }
    }
}