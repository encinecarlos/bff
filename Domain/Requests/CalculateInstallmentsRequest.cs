namespace POC.Bff.Web.Domain.Requests
{
    public sealed class CalculateInstallmentsRequest
    {
        public decimal PaymentValue { get; set; } = decimal.Zero;

        public int Installments { get; set; }


        public void GetMaxInstallments(SicesExpressBaseRequest sicesExpressParameters)
        {
            Installments = sicesExpressParameters.MaxInstallments;
        }

        public class PaymentValueRequest
        {
            public decimal PaymentValue { get; set; } = decimal.Zero;
        }
    }
}