using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class SicesExpressBaseRequest
    {
        public bool UseCreditCardGateway { get; set; }
        public bool UsePaymentSlipGateway { get; set; }
        public string GatewayUrl { get; set; }
        public int MaxInstallments { get; set; }
        public int MaxCreditCardsByPayment { get; set; }
        public GetNetConfigRequest GetNetConfig { get; set; }
    }

    public class GetNetConfigRequest
    {
        public Guid ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string SellerId { get; set; }
        public string ApiUrl { get; set; }
        public string LoaderUrl { get; set; }
        public int DaysToExpirePaymentSlip { get; set; }
    }
}