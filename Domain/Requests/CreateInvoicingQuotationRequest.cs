using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class CreateInvoicingQuotationRequest
    {
        public int Type { get; set; }
        public object SelectedInvoicingDetail { get; set; }
        public List<object> LiabilityTermFiles { get; set; }
    }
}