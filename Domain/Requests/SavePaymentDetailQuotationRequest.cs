using POC.Bff.Web.Domain.Fixed;
using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class SavePaymentDetailQuotationRequest
    {
        public PaymentMethodType PaymentMethodType { get; set; }

        public DiscountSicesRequest DiscountSices { get; set; }

        public object Addition { get; set; }

        public Guid PaymentConditionId { get; set; }
    }
}