using Discount.gRPC.Data;
using Discount.gRPC.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Services
{
    public class DiscountService(DiscountDbContext dbContext) : Discount.DiscountBase

    {
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var couponModel = request.Coupon;
            if (couponModel == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon cannot be null"));
            }
            var coupon = couponModel.Adapt<Coupon>();
            await dbContext.Coupons.AddAsync(coupon);
            await dbContext.SaveChangesAsync();
            return coupon.Adapt<CouponModel>();
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
            }
            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();
            return new DeleteDiscountResponse
            {
                Success = true
            };
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await  dbContext.Coupons.FirstOrDefaultAsync(x=>x.ProductName == request.ProductName);
            if(coupon is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
            }
            var coupnModel = coupon.Adapt<CouponModel>();

            return coupnModel;
        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var couponModel = request.Coupon;

            if (couponModel == null)
            {
                throw new RpcException(
                    new Status(StatusCode.InvalidArgument, "Coupon cannot be null"));
            }

            var existingCoupon = await dbContext.Coupons
                .FirstOrDefaultAsync(x => x.Id == couponModel.Id);

            if (existingCoupon == null)
            {
                throw new RpcException(
                    new Status(StatusCode.NotFound, "Coupon not found"));
            }

            existingCoupon.ProductName = couponModel.ProductName;
            existingCoupon.Amount = (decimal)couponModel.Amount;
            existingCoupon.Description = couponModel.Description;

            await dbContext.SaveChangesAsync();

            return existingCoupon.Adapt<CouponModel>();
        }
    }
}
