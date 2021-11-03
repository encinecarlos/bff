using POC.Shared.Domain.Fixed;
using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class ComponentListBySearchTermComponentRequest : BasePagedRequest
    {
        public string SearchTerm { get; set; }
        public int Tier { get; set; }
        public Guid DistributionCenterId { get; set; }
        public ComponentType ComponentType { get; set; }
    }
}