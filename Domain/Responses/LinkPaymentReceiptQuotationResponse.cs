using System;

namespace POC.Bff.Web.Domain.Responses
{
    public class LinkPaymentReceiptQuotationResponse
    {
        public Guid Id { get; set; }
        public int Status { get; set; }
        public object Files { get; set; }
        public TermFileResponse ProformaFile { get; set; }
    }
}