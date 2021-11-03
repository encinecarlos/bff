using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class AddLoyaltyPointsEntryRequest
    {
        public Guid OrganizationId { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public int Points { get; set; }
    }
}