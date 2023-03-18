using IS.Order.Application.Features.Orders;

namespace IS.Order.Application.Contracts;

public interface IOrderService
{
    Task<IReadOnlyList<Domain.Entities.Order>> GetAllOrders();

    Task<Guid> CreateOrderAsync(PlaceOrderInDto placeOrderInDto,
        CancellationToken cancellationToken);

    Task<Domain.Entities.Order> GetByGuid(Guid guid);

    Task RemoveOrder(Domain.Entities.Order order);
}