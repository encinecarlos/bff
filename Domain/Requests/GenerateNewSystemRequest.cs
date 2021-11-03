using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class GenerateNewSystemRequest : SystemParameterRequest
    {
        public Guid QuotationId { get; set; }
    }
}