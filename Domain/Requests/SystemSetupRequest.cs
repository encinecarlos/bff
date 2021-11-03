using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class SystemComponentRequest
    {
        public Guid DistributionCenterId { get; set; }
        public int TierId { get; set; }
        public IEnumerable<string> ErpCodes { get; set; }
        public Guid? PromotionId { get; set; }
        public bool AutomaticSystem { get; set; }
    }
}