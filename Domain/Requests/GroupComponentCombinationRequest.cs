using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class GroupComponentCombinationRequest
    {
        public string ErpCode { get; set; }

        public List<Guid> DistributionCenters { get; set; } = new List<Guid>();

        public List<int> Tiers { get; set; } = new List<int>();

        public List<Guid> Powers { get; set; } = new List<Guid>();
    }
}