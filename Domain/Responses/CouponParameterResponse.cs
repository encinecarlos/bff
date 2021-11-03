namespace POC.Bff.Web.Domain.Responses
{
    public class CouponParameterResponse
    {
        public bool? IsCouponModuleActive { get; set; }
        public int? MaxCouponPercentageDiscount { get; set; }
        public int? LoyaltyPointsInterval { get; set; }
    }
}
