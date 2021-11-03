namespace POC.Bff.Web.Domain.Requests
{
    public class ComponentValuesRequest
    {
        public string ErpCode { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }
        public decimal Power { get; set; }
        public int Type { get; set; }
        public bool IsOutOfStock { get; set; }
    }
}