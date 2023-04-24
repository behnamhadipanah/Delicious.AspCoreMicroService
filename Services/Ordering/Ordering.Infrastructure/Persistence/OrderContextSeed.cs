using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence;

public class OrderContextSeed
{
    public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
    {
        if (!await orderContext.Orders.AnyAsync())
        {
            await orderContext.Orders.AddRangeAsync(GetPreConfiguredOrders());
            await orderContext.SaveChangesAsync();
            logger.LogInformation("Data Seed Section Configured");

        }
    }

    private static IEnumerable<Order> GetPreConfiguredOrders()
    {
        return new List<Order>
        {
            new Order()
            {
                FirstName = "Behnam",
                LastName = "Hadipanah",
                Username = "behnamhadipanah",
                EmailAddress = "behnamhadipanah@gmail.com",
                City = "Tehran",
                Country = "Iran",
                TotalPrice = 1000,
                BankName = "SamanBank",
                PaymentMethod = 1,
                RefCode="132548MAS48",
                CreatedBy = Guid.NewGuid().ToString(),
                LastModifiedBy = Guid.NewGuid().ToString(),
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now



            },
        };
    }
}