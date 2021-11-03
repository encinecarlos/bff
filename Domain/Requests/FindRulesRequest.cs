using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class FindRulesRequest
    {
        public FindRulesRequest(IEnumerable<Guid> ruleIds)
        {
            RuleIds = ruleIds;
        }

        public IEnumerable<Guid> RuleIds { get; set; }
    }
}