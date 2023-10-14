using Application;
using Infrastructure;
using Presentation;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.Scan(selector => selector
            .FromAssemblies(Infrastructure.AssemblyReference.Assembly)
            .AddClasses(false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

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

            app.UseEndpoints(x => x.MapControllers());
            app.MapControllers();

            app.Run();
        }
    }
}