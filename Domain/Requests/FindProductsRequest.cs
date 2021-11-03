using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class FindProductsRequest : BasePagedRequest
    {
        public int? ProductType { get; set; }
        public string SearchTerm { get; set; }
        public decimal? PowerMin { get; set; }
        public decimal? PowerMax { get; set; }
        public int? ComponentType { get; set; }
        public Guid? DistributionCenterId { get; set; }
        public IReadOnlyCollection<int> ProductStatusTypes { get; set; }
        public Guid? SicesExpressDistributionCenterId { get; set; }

    }
}