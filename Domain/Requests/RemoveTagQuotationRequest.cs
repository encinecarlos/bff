using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class RemoveTagQuotationRequest
    {
        public RemoveTagQuotationRequest(List<Guid> quotationIds)
        {
            QuotationIds = quotationIds;
        }

        public List<Guid> QuotationIds { get; set; }
    }
}