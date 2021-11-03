namespace POC.Bff.Web.Domain.Requests
{
    public class CalculateQuotationPriceCommercialRequest
    {
        public DiscountSicesRequest DiscountSices { get; set; }
        public object Addition { get; set; }
        public decimal? PointDiscountValue { get; set; }
    }
}