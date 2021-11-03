using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class ComponentCombinationRequest
    {
        public string ErpCode { get; set; }

        public Guid? DistributionCenterId { get; set; }

        public int? TierId { get; set; }

        public List<Guid> Powers { get; set; }
    }
}