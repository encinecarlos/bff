using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class PaymentResponse
    {
        public DiscountResponse DiscountSices { get; set; }

        public object Addition { get; set; }

        public object PaymentCondition { get; set; }

        public int PaymentMethod { get; set; }

        public string PaymentTerms { get; set; }

        public IEnumerable<TermFileResponse> PaymentReceiptFiles { get; set; }

        public bool IsCompleted { get; set; }

        public bool CanEdit { get; set; }

        public bool CanUploadPaymentReceipt { get; set; }

        public List<CreditCardResponse> CreditCardList { get; set; }
    }
}