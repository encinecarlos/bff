using POC.Bff.Web.Domain.Fixed;
using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class SavePaymentDetailQuotationCommercialRequest
    {
        public PaymentMethodType PaymentMethodType { get; set; }
        public DiscountSicesRequest DiscountSices { get; set; }
        public object Addition { get; set; }
        public Guid PaymentConditionId { get; set; }
        public decimal? PointDiscountValue { get; set; }
        public int OrganizationLoyaltyBalance { get; set; }
        public decimal MaxDiscountPercent { get; set; }
    }
}