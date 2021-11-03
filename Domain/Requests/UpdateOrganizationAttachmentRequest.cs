using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class UpdateOrganizationAttachmentRequest
    {
        public Guid OrganizationId { get; set; }
        public string KeyName { get; set; }
        public string NewName { get; set; }
    }
}