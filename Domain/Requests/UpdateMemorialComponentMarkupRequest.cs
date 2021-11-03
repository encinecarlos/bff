using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class UpdateMemorialComponentMarkupRequest
    {
        public Guid? PowerId { get; set; }

        public decimal? Markup { get; set; }

        public Guid? DistributionCenterId { get; set; }

        public string Token { get; set; }

        public int? TierId { get; set; }

        public Guid? PromotionId { get; set; }
    }
}