using POC.Shared.Domain.ValueObjects;

namespace POC.Bff.Web.Domain.Requests
{
    public class ShippingDetailRequest
    {
        public Address Address { get; set; }
    }
}