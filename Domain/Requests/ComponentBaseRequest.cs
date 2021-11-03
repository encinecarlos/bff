using POC.Shared.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class ComponentBaseRequest
    {
        public int Position { get; set; }

        public string DataSheetKeyName { get; set; }

        public string ImageKeyName { get; set; }

        public object SystemPowerForSelection { get; set; }

        public string ErpCode { get; set; }

        public Text Description { get; set; }

        public Text Observations { get; set; }

        public Guid ManufacturerId { get; set; }

        public List<object> Dependencies { get; set; }

        public List<int> PricingTiers { get; set; }

        public List<int> GeneratorTiers { get; set; }

        public List<Guid> DistributionCenters { get; set; }
    }
}