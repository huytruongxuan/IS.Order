using System.Text;
using System.Text.Json;
using IS.Order.API.IntegrationTest.Base;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace IS.Order.API.IntegrationTest.Controllers;


public class OrderControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public OrderControllerTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task PlaceOrder_Success()
    {
        var client = _factory.GetAnonymousClient();

        var mockTime = DateTime.Now.AddMinutes(3).ToString("yyyy-MM-ddTHH:mm:ssK");

        var testPayload = _testPayload.Replace("MockTime", mockTime);

        await CheckAndDeleteExistingOrder("DFC1234");

        HttpContent content = new StringContent(testPayload, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/Order", content);
        
        var rs = await response.Content.ReadAsStringAsync();

        
        response.EnsureSuccessStatusCode();
    }


    private async Task CheckAndDeleteExistingOrder(string orderId)
    {
        var client = _factory.GetAnonymousClient();

        await client.DeleteAsync($"/Order?orderNumber={orderId}");
    }

    private readonly string _testPayload = "{\n  \"requestId\": \"3fa85f64-5717-4562-b3fc-2c963f66afa6\",\n  \"orderNumber\": \"DFC1234\",\n  \"customerId\": \"ABC58891\",\n  \"amount\": 100000000,\n  \"fundId\": 1121,\n  \"orderDate\": \"MockTime\"\n}";
    
}