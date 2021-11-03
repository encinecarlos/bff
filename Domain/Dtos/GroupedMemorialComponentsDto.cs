using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Dtos
{
    public class GroupedMemorialComponentsDto
    {
        public string ErpCode { get; set; }

        public string Model { get; set; }

        public decimal CmvCombinationNumber => DistributionCenters.Count * Tiers.Count;

        public List<Guid> DistributionCenters { get; set; } = new List<Guid>();

        public List<Guid> Powers { get; set; } = new List<Guid>();

        public List<int> Tiers { get; set; } = new List<int>();
    }
}