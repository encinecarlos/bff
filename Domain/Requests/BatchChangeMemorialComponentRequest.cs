using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class BatchChangeMemorialComponentRequest
    {
        public decimal? Cmv { get; set; }

        public decimal? PercentCmv { get; set; }

        public List<GroupComponentCombinationRequest> CombinationGroups { get; set; }

        public List<ComponentCombinationRequest> Combinations { get; set; }

        public string Token { get; set; }

        public Guid? PromotionId { get; set; }
    }
}