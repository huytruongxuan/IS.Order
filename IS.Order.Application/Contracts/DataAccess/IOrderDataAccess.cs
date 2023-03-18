using IS.Order.Application.Contracts.Persistence;

namespace IS.Order.Application.Contracts.DataAccess;

public interface IOrderDataAccess : IDisposable
{
    int SaveChanges();
    IOrderRepository OrderRepository { get; }
}