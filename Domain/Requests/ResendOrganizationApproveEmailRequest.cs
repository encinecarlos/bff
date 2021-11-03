using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class ResendOrganizationApproveEmailRequest
    {
        public Guid OrganizationId { get; set; }
    }
}