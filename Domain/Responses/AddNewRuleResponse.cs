using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class AddNewRuleResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<object> PercentRanges { get; set; }
    }
}