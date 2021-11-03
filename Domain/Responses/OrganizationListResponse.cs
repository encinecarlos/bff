using System;

namespace POC.Bff.Web.Domain.Responses
{
    public class OrganizationListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public object Document { get; set; }
        public string StateCode { get; set; }
        public int TierId { get; set; }
        public DateTime? IgnoreTierChangeUntil { get; set; }
        public int? LoyaltyPoints { get; set; }
        public string Email { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? Status { get; set; }
        public string TradeName { get; set; }
    }
}