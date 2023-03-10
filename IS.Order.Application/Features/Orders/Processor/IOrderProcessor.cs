namespace IS.Order.Application.Features.Orders.Processor;

public interface IOrderProcessor
{
    Task<Domain.Entities.Order> ProcessNewOrder(PlaceOrderInDto placeOrderInDto, CancellationToken cancellationToken);
}