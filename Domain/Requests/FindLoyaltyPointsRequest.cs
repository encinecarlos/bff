namespace POC.Bff.Web.Domain.Requests
{
    public class FindLoyaltyPointsRequest
    {
        public int OrganizationLoyaltyBalance { get; set; }
        public int PointsInterval { get; set; }
        public decimal MaxDiscountPercent { get; set; }
        public decimal PointDiscountValue { get; set; }
    }
}