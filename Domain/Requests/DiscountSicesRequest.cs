using POC.Bff.Web.Domain.Fixed;

namespace POC.Bff.Web.Domain.Requests
{
    public class DiscountSicesRequest
    {
        public DiscountSicesType Type { get; set; }
        public CouponRequest Coupon { get; set; }
        public int Points { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal? DiscountPercent { get; set; }
        public int MaxCouponPercentageDiscount { get; set; }
        public string CouponCode { get; set; }

    }


}