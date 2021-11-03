using POC.Bff.Web.Domain.Responses;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Dtos
{
    public class PaymentDto
    {
        public DiscountDto Discount { get; set; }

        public object Addition { get; set; }

        public object PaymentCondition { get; set; }

        public int PaymentMethod { get; set; }

        public string PaymentTerms { get; set; }

        public List<TermFileResponse> PaymentReceiptFiles { get; set; }

        public bool IsCompleted { get; set; }

        public bool CanEdit { get; set; }

        public bool CanUploadPaymentReceipt { get; set; }

        public List<CreditCardDto> CreditCards { get; set; }
    }
}