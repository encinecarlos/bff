using POC.Bff.Web.Domain.Fixed;
using POC.Bff.Web.Domain.Responses;

namespace POC.Bff.Web.Domain.Dtos
{
    public class DiscountDto
    {
        public DiscountSicesType Type { get; set; }
        public DiscountResponse.CouponDetail Coupon { get; set; }
        public int Points { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal MaxCouponPercentageDiscount { get; set; }

    }
}