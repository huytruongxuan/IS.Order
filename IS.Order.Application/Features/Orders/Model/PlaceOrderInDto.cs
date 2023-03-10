namespace IS.Order.Application.Features.Orders;

public class PlaceOrderInDto
{
    public Guid RequestId { get; set; }
    public string? OrderNumber { get; set; }
    public string? CustomerId { get; set; }
    public decimal Amount { get; set; }
    public int FundId { get; set; }
    
    public DateTime OrderDate { get; set; }
}