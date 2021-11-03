using POC.Bff.Web.Domain.Requests;
using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class AvailablePromotionResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<FeatureRequest> FeatureList { get; set; }
        public decimal MinPower { get; set; }
        public decimal MaxPower { get; set; }
        public Guid MemorialId { get; set; }
    }
}