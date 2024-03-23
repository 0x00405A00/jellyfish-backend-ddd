using Application.Shared.Mapper;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Application.Mapper
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainModelToDTOMappingProfile(provider.GetService<IConfiguration>()));
            }).CreateMapper());
            return services;    
        }
    }
}
