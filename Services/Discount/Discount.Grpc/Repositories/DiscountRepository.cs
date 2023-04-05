using Dapper;
using Discount.Grpc.Entities;
using Npgsql;

namespace Discount.Grpc.Repositories;

public class DiscountRepository : IDiscountRepository
{
    #region Constructor

    private readonly IConfiguration _configuration;

    public DiscountRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    #endregion

    public async Task<Coupon> GetDiscount(string productName)
    {
        using var connection = new NpgsqlConnection
            (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(
            "SELECT * FROM Coupon WHERE ProductName=@ProductName",
            new { ProductName = productName });

        if (coupon is null)
            return new Coupon()
            {
                ProductName = "No Discount",
                Description = "No Discount Description",
                Amount = 0
            };

        return coupon;

    }


    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        using var connection = new NpgsqlConnection
            (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
       var result= await connection.ExecuteAsync(
            "INSERT INTO Coupon (ProductName,Description,Amount) VALUES (@ProductName,@Description,@Amount)",
            new {ProductName=coupon.ProductName,Description=coupon.Description,Amount=coupon.Amount});
       if (result == 0) return false;

       return true;

    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        using var connection = new NpgsqlConnection
            (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var result = await connection.ExecuteAsync(
            "Update Coupon SET ProductName=@ProductName,Description=@Description,Amount=@Amount WHERE Id=@CouponID",
            new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, CouponId=coupon.Id });
        if (result == 0) return false;
        return true;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        using var connection = new NpgsqlConnection
            (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var result = await connection.ExecuteAsync("DELETE FROM Coupon Where ProductName=@ProductName",
            new { ProductName = productName });

        if (result == 0) return false;
        return true;
    }


}