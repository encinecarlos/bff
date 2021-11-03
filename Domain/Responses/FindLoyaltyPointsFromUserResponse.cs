namespace POC.Bff.Web.Domain.Responses
{
    public class FindLoyaltyPointsFromUserResponse
    {
        public string Country { get; set; }
        public string CurrencyCode { get; set; }
        public int PointsInterval { get; set; }
        public decimal MaxDiscountPercent { get; set; }
        public decimal PointDiscountValue { get; set; }
    }
}