using Application.Behaviour;
using Application.Mapper;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, Assembly[] assemblies)
        {
            //services.AddAutoMapper(typeof(DomainModelToDTOMappingProfile));

            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainModelToDTOMappingProfile(provider.GetService<IConfiguration>()));
            }).CreateMapper());

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(assemblies);
                config.AddOpenBehavior(typeof(PrepareResponseForPresentationPipelingBehaviour<,>));
                config.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
                config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
            });
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelingBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PrepareResponseForPresentationPipelingBehaviour<,>));
            services.AddValidatorsFromAssembly(AssemblyReference.Assembly);
            return services;    

        }
    }
}
