using Application.Behaviour;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SharedApplication = Shared.Application;
using Shared.Application.Mapper;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, Assembly[] assemblies)
        {
            services.AddAutoMapper();

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(assemblies);
                config.AddOpenBehavior(typeof(ValidationPipelingBehaviour<,>));
                config.AddOpenBehavior(typeof(PrepareResponseForPresentationPipelingBehaviour<,>));
                config.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
                config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
            });
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelingBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PrepareResponseForPresentationPipelingBehaviour<,>));
            services.AddValidatorsFromAssemblies(assemblies);
            return services;    

        }
    }
}
