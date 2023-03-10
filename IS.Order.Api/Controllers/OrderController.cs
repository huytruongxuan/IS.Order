using IS.Order.Application.Contracts;
using IS.Order.Application.Features.Orders;
using Microsoft.AspNetCore.Mvc;

namespace IS.Order.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

   [HttpGet]
    public async Task<IActionResult> GetByGuid(Guid guid){
        var order = await _orderService.GetByGuid(guid);
        return order != null ? Ok(order): BadRequest();
    }
    
    [HttpPost]
    public async Task<String> PlaceOrder(PlaceOrderInDto request)
    {
        await _orderService.CreateOrderAsync(request, CancellationToken.None);
        return "done";
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveOrder(string orderNumber)
    {
        var orders = await _orderService.GetAllOrders();
        var order = orders.FirstOrDefault(o => o.OrderNumber == orderNumber);
        if (order is not null)
        {
            await _orderService.RemoveOrder(order);
            return NoContent();
        }
        else return NotFound();
    }
}