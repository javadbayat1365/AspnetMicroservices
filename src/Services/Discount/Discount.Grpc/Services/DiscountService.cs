using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Discount.Grpc.Services;

public class DiscountService: DiscountProtoService.DiscountProtoServiceBase
{
    private readonly IDiscountRepository _repository;
    private readonly ILogger<DiscountService> _logger;
    private readonly IMapper _mapper;

    public DiscountService(ILogger<DiscountService> logger, IDiscountRepository repository, IMapper mapper)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public override  Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = _repository.GetDiscount(request.ProductName);
        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound,$"the producut name {request.ProductName} does not exist!"));
        }
        _logger.LogInformation($"Discount Is Retrive By Name : {request.ProductName}");

        var couponModel = _mapper.Map<CouponModel>(coupon);
        return Task.FromResult(couponModel);
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    { 
        var coupon = _mapper.Map<Coupon>(request.Coupon);

        await _repository.CreateDiscount(coupon);
        _logger.LogInformation($"Discount Is Created By Id : {coupon.Id}");
        var couponModel = _mapper.Map<CouponModel>(coupon);
        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request.Coupon);

       var result = await _repository.UpdateDiscount(coupon);
        if (result)
        {
            _logger.LogInformation($"Discount Is Updated By Id : {coupon.Id}");

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }
        return null;
        
    }
    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var result = await _repository.DeleteDiscount(request.Coupon.ProductName);
        _logger.LogInformation($"Discount Is Deleted By Name : {request.Coupon.ProductName}");

        return new DeleteDiscountResponse() { Success = result }; 
    }
}
