using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IS.Order.Message;

public static class MessageServiceRegistration
{
    public static IServiceCollection AddMessageServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(configurator =>
        {
            configurator.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host(configuration.GetConnectionString("MessageEndpoint"));
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }

}