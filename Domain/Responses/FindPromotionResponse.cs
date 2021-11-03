using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public sealed class FindPromotionResponse
    {
        public long Total { get; set; }
        public IEnumerable<PagedPromotionList> List { get; set; }
    }

    public class PagedPromotionList
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpirationAt { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public IEnumerable<PagedPromotionTierList> TierList { get; set; }
    }

    public class PagedPromotionTierList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
    }
}