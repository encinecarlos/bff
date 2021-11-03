using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class AvailablePromotionCommercialResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<FeaturePromotion> FeatureList { get; set; }
        public decimal MinPower { get; set; }
        public decimal MaxPower { get; set; }
        public Guid MemorialId { get; set; }

        public class FeaturePromotion
        {
            public bool LinkEnabled { get; set; }
            public string Description { get; set; }
            public Link Link { get; set; }
        }

        public class Link
        {
            public string FileName { get; set; }
            public string KeyName { get; set; }
        }
    }
}