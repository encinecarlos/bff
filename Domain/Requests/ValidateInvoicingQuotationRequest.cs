using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class ValidateInvoicingQuotationRequest
    {
        public int InvoicingPersonType { get; set; }

        public List<object> LiabilityTermFiles { get; set; }
    }
}