namespace POC.Bff.Web.Domain.Requests
{
    public class SystemQuotationRequest
    {
        public SystemRequest PvSystem { get; set; }
        public LoyaltyPointsConfigurationRequest LoyaltyPointsConfiguration { get; set; }
    }
}