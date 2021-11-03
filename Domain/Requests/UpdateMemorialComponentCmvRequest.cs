using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class UpdateMemorialComponentCmvRequest
    {
        public decimal? Cmv { get; set; }

        public string Token { get; set; }

        public Guid? DistributionCenterId { get; set; }

        public int? TierId { get; set; }

        public Guid? PromotionId { get; set; }
    }
}