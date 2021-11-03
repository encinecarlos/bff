using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class ResendOrganizationConfirmationEmailRequest
    {
        public Guid OrganizationId { get; set; }
    }
}