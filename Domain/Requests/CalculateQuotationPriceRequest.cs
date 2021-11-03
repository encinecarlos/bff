namespace POC.Bff.Web.Domain.Requests
{
    public class CalculateQuotationPriceRequest
    {
        public DiscountSicesRequest DiscountSices { get; set; }
        public object Addition { get; set; }

    }
}