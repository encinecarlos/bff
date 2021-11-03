using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class InvoicingResponse
    {
        public int Type { get; set; }

        public IEnumerable<TermFileResponse> LiabilityTermFiles { get; set; }
        public IEnumerable<TermFileResponse> InvoiceList { get; set; }
        public List<TermFileResponse> QuotationFiles { get; set; }
        public DateTime? InvoicedAt { get; set; }
        public object SelectedInvoicingDetail { get; set; }

        public bool IsCompleted { get; set; }

        public bool CanEdit { get; set; }
    }
}