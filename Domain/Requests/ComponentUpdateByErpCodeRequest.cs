using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class ComponentUpdateByErpCodeRequest
    {
        public List<int> PricingTiers { get; set; }
        public List<int> GeneratorTiers { get; set; }
        public List<Guid> DistributionCenters { get; set; }
    }
}