using POC.Bff.Web.Domain.Requests;
using POC.Shared.Responses;
using System;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Coupon
{
    public interface ICouponService
    {
        Task<Response> GetCoupon(Guid id);
        Task<Response> GetCouponByCode(string couponCode);
        Task<Response> AddCoupon(CouponRequest command);
        Task<Response> UpdateCoupon(Guid id, CouponRequest command);
        Task<Response> FindAllCoupons(FindAllCouponRequest query);
        Task<Response> DeleteCoupon(Guid id);
        Task<Response> CloneCoupon(CloneCouponRequest request);
    }
}
