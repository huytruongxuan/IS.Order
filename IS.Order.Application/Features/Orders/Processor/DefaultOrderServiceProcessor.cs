using AutoMapper;
using IS.Order.Application.Contracts.DataAccess;
using IS.Order.Application.Features.Orders.Validator;

namespace IS.Order.Application.Features.Orders.Processor;

public class DefaultOrderServiceProcessor : IOrderProcessor
{
    private readonly IOrderDataAccess _orderDataAccess;
    private readonly IMapper _mapper;
    
    public DefaultOrderServiceProcessor(IMapper mapper, IOrderDataAccess orderDataAccess)
    {
        _mapper = mapper;
        _orderDataAccess = orderDataAccess;
    }
    
    public async Task<Domain.Entities.Order> ProcessNewOrder(PlaceOrderInDto placeOrderInDto, CancellationToken cancellationToken)
    {
        var @order = _mapper.Map<Domain.Entities.Order>(placeOrderInDto);

        var validation = new OrderPlacementRequestValidator(_orderDataAccess.OrderRepository);
        var validationResult = await validation.ValidateAsync(@order, cancellationToken);
        if (validationResult.Errors.Count > 0)
        {
            throw new Exceptions.ValidationException(validationResult);
        }

        order.Created = DateTime.Now;
        return await _orderDataAccess.OrderRepository.AddAsync(@order);
    }
}
