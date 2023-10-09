using Mapster;
using MapsterMapper;
using ToDo.Application.MappingConfig;

namespace ToDo.DI
{
    public static class MappingExtension
    {
        public static IServiceCollection AddMappingService(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;

            config.Scan(typeof(ApplicationUserMappingConfig).Assembly);

            services.AddSingleton(config);

            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}
