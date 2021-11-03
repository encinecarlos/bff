using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class FindInsuranceByRangesRequest
    {
        public string OrganizationId { get; set; }

        public string Name { get; set; }

        public Guid PromotionId { get; set; }

        public decimal Power { get; set; }

        public decimal PvSystemPrice { get; set; }

        public Guid DistributionCenterId { get; set; }

        public int TierId { get; set; }
    }
}