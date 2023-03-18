using IS.Order.Application.Contracts.DataAccess;
using IS.Order.DataAccess.Order;
using Microsoft.Extensions.DependencyInjection;

namespace IS.Order.DataAccess;

public static class DataAccessServiceRegistration
{
    public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderDataAccess, OrderDataAccess>();

        return services;
    }
}