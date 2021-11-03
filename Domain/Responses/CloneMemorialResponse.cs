using System;

namespace POC.Bff.Web.Domain.Responses
{
    public sealed class CloneMemorialResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public Guid CreateBy { get; set; }
        public DateTime EstimatedExpirationAt { get; set; }
        public Guid? PublishedBy { get; set; }
        public Guid? ExpiredBy { get; set; }
        public DateTime? PublishedAt { get; set; }
        public DateTime? ExpiredAt { get; set; }
        public bool IsPromotion { get; set; }
        public string PromotionCode { get; set; }
        public string CountryCode { get; set; }
        public string CurrencyCode { get; set; }
    }
}