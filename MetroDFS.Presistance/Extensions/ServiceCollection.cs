using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace MetroDFS.Presistance.Extensions
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddPresistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddContext(configuration);
            return services;
        }
        private static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DataBase");

            services.AddDbContext<MetroDFSContext>(options =>
               options.UseLazyLoadingProxies().UseSqlServer(connectionString,
                   builder => builder.MigrationsAssembly(typeof(MetroDFSContext).Assembly.FullName)));

            return services;
        }
    }
}
