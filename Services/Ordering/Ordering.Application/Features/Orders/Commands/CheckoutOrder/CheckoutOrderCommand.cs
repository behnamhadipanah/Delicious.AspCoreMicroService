using MediatR;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder;

public class CheckoutOrderCommand:IRequest<int>
{
    public string Username { get; set; }
    public long TotalPrice { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string BankName { get; set; }
    public string RefCode { get; set; }
    public int PaymentMethod { get; set; }

    public CheckoutOrderCommand(string username, long totalPrice, string firstName, string lastName, string emailAddress, string country, string city, string bankName, string refCode, int paymentMethod)
    {
        Username = username;
        TotalPrice = totalPrice;
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
        Country = country;
        City = city;
        BankName = bankName;
        RefCode = refCode;
        PaymentMethod = paymentMethod;
    }
}