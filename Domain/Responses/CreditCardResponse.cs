namespace POC.Bff.Web.Domain.Responses
{
    public class CreditCardResponse
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public int PaymentAmount { get; set; }
        public int Parcels { get; set; }
        public decimal ParcelValue { get; set; }
    }
}