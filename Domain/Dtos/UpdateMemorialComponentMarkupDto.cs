using System;

namespace POC.Bff.Web.Domain.Dtos
{
    public class UpdateMemorialComponentMarkupDto
    {
        public Guid? PowerId { get; set; }

        public decimal? Markup { get; set; }

        public Guid? CdId { get; set; }

        public string Token { get; set; }

        public int? TierId { get; set; }

        public Guid? PromotionId { get; set; }
    }
}