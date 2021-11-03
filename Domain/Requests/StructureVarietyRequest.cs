using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class StructureVarietyRequest
    {
        public object Name { get; set; }

        public object Description { get; set; }

        public int Position { get; set; }

        public object PhysicalParameters { get; set; }

        public object SolverFunctions { get; set; }

        public List<object> RawMaterialInfo { get; set; }

        public List<int> PricingTiers { get; set; }

        public List<int> GeneratorTiers { get; set; }

        public List<Guid> DistributionCenterIds { get; set; }
    }
}