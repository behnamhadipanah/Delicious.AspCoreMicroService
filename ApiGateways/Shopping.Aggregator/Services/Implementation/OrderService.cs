using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services.Implementation;

public class OrderService : IOrderService
{
    private readonly HttpClient _client;

    public OrderService(HttpClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<OrderResponseModel>> GetOrderByUserName(string username)
    {
        var response = await _client.GetAsync($"/api/v1/Order/{username}");
        return await response.ReadContentAs<List<OrderResponseModel>>();
    }
}
