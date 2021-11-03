namespace POC.Bff.Web.Domain.Responses
{
    public class ProductsOrderResponse
    {
        public string ErpCode { get; set; }
        public ImageDetailResponse ImageDetail { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Power { get; set; }
    }
}