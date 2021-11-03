using POC.Shared.Responses;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class CalculateShippingQuotationResponse
    {
        public CalculateShippingQuotationResponse(
            decimal? shippingPrice,
            bool freeShipping,
            List<NotificationResponse> notifications)
        {
            ShippingPrice = shippingPrice;
            FreeShipping = freeShipping;
            Notifications = notifications ?? new List<NotificationResponse>();
        }

        public decimal? ShippingPrice { get; set; }
        public bool FreeShipping { get; set; }
        public List<NotificationResponse> Notifications { get; set; }
    }
}