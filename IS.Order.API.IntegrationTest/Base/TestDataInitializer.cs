using IS.Order.Persistence;

namespace IS.Order.API.IntegrationTest.Base;

public class TestDataInitializer
{
    public static void InitializeDbForTests(ApplicationDbContext context)
    {
        context.Orders.Add(new Domain.Entities.Order()
        {
            Id = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}"),
            OrderNumber = "12345",
            CustomerId = "1121",
            OrderDate = DateTime.Now.Add(TimeSpan.FromDays(1)),
            Amount = 1000000,
            FundId = 1121
        });


        context.SaveChanges();
    }
}