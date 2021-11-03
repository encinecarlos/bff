using POC.Shared.Domain.ValueObjects;

namespace POC.Bff.Web.Domain.Requests
{
    public class ComponentOrderRequest
    {
        public ErpCode ErpCode { get; set; }

        public int Quantity { get; set; }
    }
}