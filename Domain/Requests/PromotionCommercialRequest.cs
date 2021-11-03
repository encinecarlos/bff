using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public sealed class PromotionCommercialRequest
    {
        public string Name { get; set; }
        public bool ExpirationEnabled { get; set; }
        public DateTime? ExpirationAt { get; set; }
        public string Description { get; set; }
        public List<FeatureRequest> FeatureList { get; set; }
        public decimal? MinPower { get; set; }
        public decimal? MaxPower { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool FreeShipping { get; set; }
        public decimal? FreeShippingMaxPower { get; set; }
        public bool FreeInsuranceEnabled { get; set; }
        public Guid? SelectedFreeInsuranceId { get; set; }
        public ComponentConfigurationCommercialRequest ComponentConfig { get; set; }
        public List<int> TierList { get; set; }
        public List<Guid> CdList { get; set; }
        public List<Guid> InsuranceList { get; set; }
        public Guid MemorialId { get; set; }
    }
}