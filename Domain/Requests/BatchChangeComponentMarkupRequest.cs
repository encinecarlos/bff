using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class BatchChangeComponentMarkupRequest
    {
        public decimal? Markup { get; set; }

        public decimal? PercentMarkup { get; set; }

        public List<GroupComponentCombinationRequest> CombinationGroups { get; set; }

        public List<ComponentCombinationRequest> Combinations { get; set; }

        public string Token { get; set; }

        public Guid? PromotionId { get; set; }
    }
}