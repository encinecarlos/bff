using POC.Bff.Web.Domain.Requests;
using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public sealed class GetPromotionByIdCommercialResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public bool ExpirationEnabled { get; set; }
        public DateTime? ExpirationAt { get; set; }
        public string Description { get; set; }
        public List<FeatureRequest> FeatureList { get; set; }
        public long MinPower { get; set; }
        public long MaxPower { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public bool FreeShipping { get; set; }
        public long FreeShippingMaxPower { get; set; }
        public bool FreeInsuranceEnabled { get; set; }
        public Guid SelectedFreeInsuranceId { get; set; }
        public List<int> TierList { get; set; }
        public List<Guid> CdList { get; set; }
        public List<Guid> InsuranceList { get; set; }
        public ComponentConfigurationCommercialRequest ComponentConfig { get; set; }
        public Guid MemorialId { get; set; }
    }
}