using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class BatchDeleteQuotationsRequest
    {
        public List<Guid> QuotationIds { get; set; }
    }
}