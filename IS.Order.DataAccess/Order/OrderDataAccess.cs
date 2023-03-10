using IS.Order.Application.Contracts.DataAccess;
using IS.Order.Application.Contracts.Persistence;
using IS.Order.Persistence;
using IS.Order.Persistence.Repositories;

namespace IS.Order.DataAccess.Order;

public class OrderDataAccess : IOrderDataAccess
{
    private IOrderRepository _orderRepository;
    private readonly ApplicationDbContext _dbContext;
    
    public OrderDataAccess(ApplicationDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public IOrderRepository OrderRepository
    {
        get {return _orderRepository??= new OrderRepository(_dbContext);}
    }


    public int SaveChanges()
    {
        return _dbContext.SaveChanges();
    }
    
    public void Dispose()
    {
        _dbContext.Dispose();
    }
    

}