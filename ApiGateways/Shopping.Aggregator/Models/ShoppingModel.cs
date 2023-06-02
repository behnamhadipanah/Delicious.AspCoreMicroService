﻿namespace Shopping.Aggregator.Models;

public class ShoppingModel
{
    public string Username { get; set; }
    public BasketModel  BasketWithProduct { get; set; }
    public IEnumerable<OrderResponseModel>  Orders { get; set; }

}
