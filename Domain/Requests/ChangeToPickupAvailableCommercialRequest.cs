using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class ChangeToPickupAvailableCommercialRequest
    {
        public Guid QuotationId { get; set; }
        public int Points { get; set; }
    }
}