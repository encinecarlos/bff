using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class FindInsuranceByRangesCommercialRequest
    {
        public List<Guid> InsuranceIds { get; set; }

        public string OrganizationId { get; set; }

        public string Name { get; set; }

        public decimal Power { get; set; }

        public decimal PvSystemPrice { get; set; }

        public Guid DistributionCenterId { get; set; }

        public int TierId { get; set; }
    }
}