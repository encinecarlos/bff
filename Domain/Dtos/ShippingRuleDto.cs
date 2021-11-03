using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Dtos
{
    public class ShippingRuleDto
    {
        public Guid Id { get; set; }
        public Guid DistributionCenterId { get; set; }
        public List<RegionRuleDto> Regions { get; set; }
    }
}