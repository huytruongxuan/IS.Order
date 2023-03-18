using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IS.Order.Cache;

public static class CacheServiceRegistration
{
    public static IServiceCollection AddCacheServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDistributedRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("CacheEndpoint");
        });
        return services;
    }
}