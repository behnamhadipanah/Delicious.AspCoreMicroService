using Discount.Grpc.Protos;

namespace Basket.Api.GrpcServices;

public class DiscountGrpcService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoServiceClient;

    public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
    {
        _discountProtoServiceClient = discountProtoServiceClient;
    }

    #region get discount

    public async Task<CouponModel> GetDiscount(string productName)
    {
        var request = new GetDiscountRequest() { ProductName = productName };
        return await _discountProtoServiceClient.GetDiscountAsync(request);
    }

    #endregion
}