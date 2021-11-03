using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POC.Bff.Web.Domain.Requests
{
    public sealed class PromotionRequest
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
        public ComponentConfigurationRequest ComponentConfig { get; set; }
        public List<int> TierList { get; set; }
        public List<Guid> CdList { get; set; }
        public List<Guid> InsuranceList { get; set; }

        [JsonIgnore] public Guid MemorialId { get; set; }

        public List<string> GetAllErpCodes()
        {
            if (ComponentConfig is null) return new List<string>(0);

            return ComponentConfig
                .ComponentGroupList
                .SelectMany(x => x.ComponentOptionList)
                .Select(x => x.ErpCode)
                .ToList();
        }

        public List<string> SubtractErpCodes(List<string> newErpCodes)
        {
            var oldErpCodes = GetAllErpCodes();

            return oldErpCodes.Except(newErpCodes).ToList();
        }
    }
}