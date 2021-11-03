using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class FetchLoyaltyPointsRequest : BasePagedRequest
    {
        public Guid OrganizationId { get; set; }
        public string SearchTerm { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Field { get; set; }
    }
}