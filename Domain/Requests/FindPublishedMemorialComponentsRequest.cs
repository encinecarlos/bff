using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class FindPublishedMemorialComponentsGroupedByTypeRequest
    {
        public Guid DistributionCenterId { get; set; }

        public int TierId { get; set; }

        public string Language { get; set; }

        public Guid MemorialId { get; set; }
    }
}