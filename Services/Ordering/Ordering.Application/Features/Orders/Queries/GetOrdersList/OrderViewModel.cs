﻿namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

public class OrderViewModel
{
    public int Id { get; set; }
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
}