using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class TransferLinkedOrganizationRequest
    {
        public Guid OriginSicesUserId { get; set; }
        public Guid DestinationSicesUserId { get; set; }
    }
}