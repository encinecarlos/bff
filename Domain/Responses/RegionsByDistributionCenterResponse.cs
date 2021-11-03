using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class RegionsByDistributionCenterResponse
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public IEnumerable<StateRuleResponse> StateList { get; set; }

        public class StateRuleResponse
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Abbreviation { get; set; }
            public IEnumerable<RegionRuleResponse> RegionList { get; set; }

            public class RegionRuleResponse
            {
                public Guid Id { get; set; }
                public string RegionName { get; set; }
                public Guid LinkedRuleId { get; set; }
                public string LinkedRuleName { get; set; }
            }
        }
    }
}