using POC.Bff.Web.Domain.Fixed;

namespace POC.Bff.Web.Domain.Responses
{
    public class DiscountResponse
    {
        public DiscountSicesType Type { get; set; }
        public CouponDetail Coupon { get; set; }
        public int Points { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal MaxCouponPercentageDiscount { get; set; }

        public class CouponDetail
        {
            public string Code { get; set; }
            public int Discount { get; set; }
        }
    }
}