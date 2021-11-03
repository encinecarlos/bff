using POC.Bff.Web.Domain.Responses;
using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class AddNewOrganizationAttachmentRequest
    {
        public Guid Id { get; set; }
        public AttachmentResponse Files { get; set; }
    }
}