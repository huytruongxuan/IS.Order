using AutoMapper;
using IS.Order.Application.Contracts;
using IS.Order.Application.Contracts.DataAccess;
using IS.Order.Application.Contracts.Persistence;
using IS.Order.Application.Features.Orders.Processor;
using IS.Order.Application.Features.Orders.Validator;

namespace IS.Order.Application.Features.Orders;

public class OrderService : IOrderService
{
    private readonly IOrderDataAccess _orderDataAccess;
    private readonly IMapper _mapper;
    private readonly IOrderProcessor _orderProcessor;
    
    public OrderService(IMapper mapper, IOrderDataAccess orderDataAccess, IOrderProcessor orderProcessor)
    {
        _mapper = mapper;
        _orderDataAccess = orderDataAccess;
        _orderProcessor = orderProcessor;
    }
  

    public async Task<Domain.Entities.Order> GetByGuid(Guid guid){
       return await _orderDataAccess.OrderRepository.GetByIdAsync(guid);
    }
    
    public async Task<Guid> CreateOrderAsync(PlaceOrderInDto placeOrderInDto, CancellationToken cancellationToken)
    {
        var order = await _orderProcessor.ProcessNewOrder(placeOrderInDto, cancellationToken);
            
        _orderDataAccess.SaveChanges();
        
        return order.Id;
    }

    public Task RemoveOrder(Domain.Entities.Order order)
        =>  _orderDataAccess.OrderRepository.DeleteAsync(order);

    public Task<IReadOnlyList<Domain.Entities.Order>> GetAllOrders()
        => _orderDataAccess.OrderRepository.ListAllAsync();
}