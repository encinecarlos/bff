using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class GetMemorialRequest
    {
        public Guid Id { get; set; }

        public string Token { get; set; }

        public string SearchTerm { get; set; }

        public List<Guid> DistributionCenters { get; set; }

        public List<int> Tiers { get; set; }

        public List<Guid> Manufacturers { get; set; }

        public List<Guid> Powers { get; set; }

        public List<int> ComponentTypes { get; set; }
    }
}