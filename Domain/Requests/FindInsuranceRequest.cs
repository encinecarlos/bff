using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class FindInsuranceRequest
    {
        public Guid PromotionId { get; set; }

        public int TierId { get; set; }

        public Guid DistributionCenterId { get; set; }

        public decimal Power { get; set; }

        public decimal PvSystemPrice { get; set; }

        public string CountryCode { get; set; }

        public string CurrencyCode { get; set; }

        public List<Guid> Ids { get; set; }
    }
}