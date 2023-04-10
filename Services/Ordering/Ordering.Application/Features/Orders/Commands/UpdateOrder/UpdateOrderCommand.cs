using MediatR;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommand:IRequest
{
    public int Id { get; set; }
    public string Username { get; set; }
    public decimal TotalPrice { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string BankName { get; set; }
    public string RefCode { get; set; }
    public int PaymentMethod { get; set; }
}