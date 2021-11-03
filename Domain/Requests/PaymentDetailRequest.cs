using POC.Bff.Web.Domain.Fixed;
using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class PaymentDetailRequest
    {
        public PaymentMethodType PaymentMethod { get; set; }
        public string BoletoUrl { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}