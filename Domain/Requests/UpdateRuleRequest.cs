using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class UpdateRuleRequest
    {
        public decimal MinPrice { get; set; }
        public decimal Markup { get; set; }
        public List<PercentRangeDetail> PercentRanges { get; set; }

        public class PercentRangeDetail
        {
            public Guid Id { get; set; }
            public decimal Percent { get; set; }
            public decimal MinPrice { get; set; }
            public decimal MaxPrice { get; set; }
        }
    }
}