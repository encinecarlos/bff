using System;

namespace POC.Bff.Web.Domain.Requests
{
    public sealed class ClonePromotionRequest
    {
        public ClonePromotionRequest(Guid memorialCloneId)
        {
            MemorialCloneId = memorialCloneId;
        }

        public Guid MemorialCloneId { get; set; }
    }
}