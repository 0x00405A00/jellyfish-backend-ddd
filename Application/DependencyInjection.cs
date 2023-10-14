using Application.Behaviour;
using Application.Mapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, Assembly[] assemblies)
        {
            services.AddAutoMapper(typeof(DomainModelToDTOMappingProfile));

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(assemblies);
                config.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
                config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
            });
            services.AddScoped(typeof(IPipelineBehavior<,>),typeof(ValidationPipelingBehaviour<,>));
            services.AddValidatorsFromAssembly(AssemblyReference.Assembly);
            return services;    

        }
    }
}
