namespace POC.Bff.Web.Domain.Requests
{
    public class LoyaltyPointsConfigurationRequest
    {
        public decimal MaxDiscountPercent { get; set; }
        public decimal PointDiscountValue { get; set; }
    }
}