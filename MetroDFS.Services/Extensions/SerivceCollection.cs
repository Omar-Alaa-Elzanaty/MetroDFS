using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MetroDFS.Services.Extensions
{
    public static class SerivceCollection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services
                .AddMediator()
                .AddMapping();

            return services;
        }

        private static IServiceCollection AddMediator(this IServiceCollection services)
        {
            return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }

        private static IServiceCollection AddMapping(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);

            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}
