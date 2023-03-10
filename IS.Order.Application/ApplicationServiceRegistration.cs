using System.Reflection;
using IS.Order.Application.Contracts;
using IS.Order.Application.Features.Orders;
using IS.Order.Application.Features.Orders.Processor;
using Microsoft.Extensions.DependencyInjection;

namespace IS.Order.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderProcessor, DefaultOrderServiceProcessor>();

        return services;
    }
}