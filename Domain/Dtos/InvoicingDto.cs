using POC.Bff.Web.Domain.Responses;
using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Dtos
{
    public class InvoicingDto
    {
        public int Type { get; set; }

        public List<TermFileResponse> LiabilityTermFiles { get; set; }

        public List<TermFileResponse> InvoiceList { get; set; }

        public List<TermFileResponse> QuotationFiles { get; set; }

        public DateTime? InvoicedAt { get; set; }

        public object SelectedInvoicingDetail { get; set; }

        public bool IsCompleted { get; set; }

        public bool CanEdit { get; set; }
    }
}