
using Microsoft.EntityFrameworkCore;
using src.Domain.Common;

namespace IS.Order.Domain.Entities;

public class Order : BaseAuditableEntity
{
    public Guid Id { get; set; }
    public string? OrderNumber { get; set; }
    public string? CustomerId { get; set; }
    
    [Precision(30,6)]
    public decimal Amount { get; set; }
    public int FundId { get; set; }
    public DateTime OrderDate { get; set; }
}