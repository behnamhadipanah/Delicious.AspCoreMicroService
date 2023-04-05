using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly IDiscountRepository _discountRepository;
    private readonly ILogger<DiscountService> _logger;
    private readonly IMapper _mapper;

    public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _logger = logger;
        _mapper = mapper;
    }

    #region Get Discount

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _discountRepository.GetDiscount(request.ProductName);
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound,
                $"Discount with product name {request.ProductName} is not found"));


        _logger.LogInformation("Discount is Retrieved for Product Name ");

        return _mapper.Map<CouponModel>(coupon);
    }

    #endregion

    #region Create Discount

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        Coupon coupon = _mapper.Map<Coupon>(request.Coupon);
        bool isSuccessCreate = await _discountRepository.CreateDiscount(coupon);
        
        if (isSuccessCreate)
            throw new RpcException(new Status(StatusCode.NotFound,
                $"Error Create Discount for  product name {coupon.ProductName}"));

        _logger.LogInformation($"Discount is Successfully Created for product {coupon.ProductName}");

        return _mapper.Map<CouponModel>(coupon);
    }

    #endregion

    #region Update Discount

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        Coupon coupon = _mapper.Map<Coupon>(request.Coupon);
        bool isSuccessUpdate = await _discountRepository.UpdateDiscount(coupon);

        if (isSuccessUpdate)
            throw new RpcException(new Status(StatusCode.NotFound,
                $"Error Update Discount for  product name {coupon.ProductName}"));

        _logger.LogInformation($"Discount is Successfully Update for product {coupon.ProductName}");

        return _mapper.Map<CouponModel>(coupon);
    }

    #endregion

    #region Delete Discount

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var isSuccessDelete = await _discountRepository.DeleteDiscount(request.ProductName);
        if (isSuccessDelete)
            throw new RpcException(new Status(StatusCode.NotFound,
                $"Error Delete Discount for  product name {request.ProductName}"));

        
        _logger.LogInformation($"Discount is Successfully Delete for product {request.ProductName}");

        return new DeleteDiscountResponse()
        {
            Success = isSuccessDelete
        };
    }

    #endregion
}