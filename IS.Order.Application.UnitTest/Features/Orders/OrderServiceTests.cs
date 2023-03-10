using AutoMapper;
using Bogus;
using IS.Order.Application.Contracts.DataAccess;
using IS.Order.Application.Contracts.Persistence;
using IS.Order.Application.Features.Orders;
using IS.Order.Application.Features.Orders.Processor;
using IS.Order.Application.Profiles;
using IS.Order.Application.UnitTest.Mocks;
using Moq;
using Shouldly;
using ValidationException = IS.Order.Application.Exceptions.ValidationException;

namespace IS.Order.Application.UnitTest.Features.Orders;

public class OrderServiceTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IOrderDataAccess> _mockOrderDataAccess;

    public OrderServiceTests()
    {
        _mockOrderDataAccess = RepositoryMocks.GetOrderDataAccess();
        var configurationProvider = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });

        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public async Task PlaceOrderTest_Success()
    {
        var processor = new DefaultOrderServiceProcessor(_mapper, _mockOrderDataAccess.Object);
        var service = new OrderService(_mapper, _mockOrderDataAccess.Object, processor);

        // var orderRequest = new OrderPlacementRequestDto
        // {
        //     CustomerId = "1121",
        //     OrderDate = DateTime.Now.Add(TimeSpan.FromDays(1)),
        //     Amount = 1000000,
        //     FundId = 1121,
        //     OrderNumber = "12345"
        // };
        var orderId = 0;
        var result = await service.CreateOrderAsync(await FakeData(orderId), CancellationToken.None);

        result.ShouldBe(Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}"));
    }
    
    [Fact]
    public async Task PlaceOrderTest_Fail_InsufficientAmount()
    {
        var processor = new DefaultOrderServiceProcessor(_mapper, _mockOrderDataAccess.Object);
        var service = new OrderService(_mapper, _mockOrderDataAccess.Object, processor);

        var orderRequest = new PlaceOrderInDto
        {
            CustomerId = "1121",
            OrderDate = DateTime.Now.Add(TimeSpan.FromDays(1)),
            Amount = 0,
            FundId = 1121,
            OrderNumber = "12345"
        };
        var ex = await Assert.ThrowsAsync<ValidationException>(() =>
             service.CreateOrderAsync(orderRequest, CancellationToken.None));

        ex.ValidationErrors[0].ShouldBe("Amount must be greater than 0.");
    }
    
    public Task<PlaceOrderInDto> FakeData(int orderId)
    {
        var customerIds = new[] { "999C010845", "999C011128", "999C011131", "999C000008", "999C000099" };
        var fundIds = new[] { 1111111, 1111112, 1111113, 1111114, 1111115 };

        var orderRequest = new Faker<PlaceOrderInDto>()
            // //Ensure all properties have rules. By default, StrictMode is false
            // //Set a global policy by using Faker.DefaultStrictMode
            // .StrictMode(true)
            //Pick some CustomerId from a basket
            .RuleFor(o => o.CustomerId, f => f.PickRandom(customerIds))
            //OrderDate is current date
            .RuleFor(o => o.OrderDate, f => DateTime.Now.Add(TimeSpan.FromDays(1)))
            //A random Amount from 1 to 1000
            .RuleFor(o => o.Amount, f => f.Random.Decimal(1, 1000))
            //Pick some FundId from a basket
            .RuleFor(o => o.FundId, f => f.PickRandom(fundIds))
            //OrderNumber is deterministic
            .RuleFor(o => o.OrderNumber, f => DateTime.Now.ToString("yyyyMMdd") + (orderId++).ToString("D6"));
        return Task.FromResult<PlaceOrderInDto>(orderRequest);
    }
}