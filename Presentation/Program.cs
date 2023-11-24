using Application;
using Infrastructure;
using Infrastructure.Healthcheck.Response;
using Infrastructure.SignalR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Presentation;
using Shared.Const;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddInfrastructure();
            builder.Services.AddApplication(new System.Reflection.Assembly[] {Application.AssemblyReference.Assembly});
            builder.Services.AddPresentation();

            var app = builder.Build();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseExceptionHandler("/error-debug");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            //Default wwwroot
            app.UseStaticFiles(); // This line enables the serving of static files from the wwwroot folder.

            app.MapHealthChecks("/healthz", new HealthCheckOptions
            {
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                },
                ResponseWriter = HealthCheckResponseWriter.WriteResponse
            }).RequireAuthorization(AuthorizationConst.Policy.AdminPolicy);

            app.UseEndpoints(x => x.MapControllers());
            app.MapControllers();

            app.MapHub<MessengerHub>("/messengerhub");

            app.Run();
        }
    }
}