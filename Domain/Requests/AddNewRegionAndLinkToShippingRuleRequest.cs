using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class AddNewRegionAndLinkToShippingRuleRequest
    {
        public Guid StateId { get; set; }
        public Guid RuleId { get; set; }
        public string RegionName { get; set; }
    }
}