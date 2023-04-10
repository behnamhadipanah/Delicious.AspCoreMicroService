using MediatR;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

public class GetOrdersListQuery:IRequest<List<OrderViewModel>>
{
    public string Username { get; set; }

    public GetOrdersListQuery(string username)
    {
        Username = username;
    }
}