using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class CreateSalesQuotationRequest
    {
        public Guid QuotationId { get; set; }

        public object SaleFiscalDetail { get; set; }
    }
}