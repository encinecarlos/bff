namespace POC.Bff.Web.Domain.Requests
{
    public class CreateInvoicingQuotationCommercialRequest : CreateInvoicingQuotationRequest
    {
        public LoyaltyPointsConfigurationRequest LoyaltyPointsConfiguration { get; set; }
    }
}