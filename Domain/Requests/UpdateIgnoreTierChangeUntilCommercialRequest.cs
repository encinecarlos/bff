using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class UpdateIgnoreTierChangeUntilCommercialRequest
    {
        public int TierId { get; set; }
        public string TierName { get; set; }
        public DateTime? IgnoreTierChangeUntil { get; set; }
    }
}