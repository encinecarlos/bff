using System;

namespace POC.Bff.Web.Domain.Dtos
{
    public class UpdateMemorialComponentCmvDto
    {
        public decimal? Cmv { get; set; }

        public string Token { get; set; }

        public Guid? CdId { get; set; }

        public int? TierId { get; set; }

        public Guid? PromotionId { get; set; }
    }
}