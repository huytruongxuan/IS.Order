using IS.Order.Application.Contracts.DataAccess;
using IS.Order.Application.Contracts.Persistence;
using Moq;

namespace IS.Order.Application.UnitTest.Mocks;

public class RepositoryMocks
{
    public static Mock<IOrderDataAccess> GetOrderDataAccess()
    {
        var guid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");

        var orderSample = new Domain.Entities.Order
        {
            Id = guid, 
            OrderNumber = "12345",
            CustomerId = "1121",
            OrderDate = DateTime.Now.Add(TimeSpan.FromDays(1)),
            Amount = 1000000,
            FundId = 1121
        };

        var mockOrderRepository = new Mock<IOrderRepository>();
        mockOrderRepository.Setup(repo => repo.AddAsync(It.IsAny<Domain.Entities.Order>()))
            .ReturnsAsync(() => orderSample);
        
        var mockOrderDataAccess = new Mock<IOrderDataAccess>();
        mockOrderDataAccess.Setup(dataAccess => dataAccess.OrderRepository)
            .Returns(mockOrderRepository.Object);

        return mockOrderDataAccess;
    }
}