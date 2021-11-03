using POC.Bff.Web.Domain.Fixed;
using System;

namespace POC.Bff.Web.Domain.Responses
{
    public class PaymentDetailResponse
    {
        public PaymentMethodType PaymentMethod { get; set; }
        public string BoletoUrl { get; set; }
        public DateTime? ExpirationAt { get; set; }
    }
}