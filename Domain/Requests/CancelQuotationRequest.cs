using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class CancelQuotationRequest
    {
        public List<Guid> QuotationIds { get; set; }
    }
}