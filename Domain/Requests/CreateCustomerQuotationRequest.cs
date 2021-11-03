using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class CreateCustomerQuotationRequest
    {
        public object Customer { get; set; }
        public bool IsCompleted { get; set; }
        public bool CanEdit { get; set; }
    }

    public class CreateQuotationRequest
    {
        public Guid OrganizationId { get; set; }
    }
}