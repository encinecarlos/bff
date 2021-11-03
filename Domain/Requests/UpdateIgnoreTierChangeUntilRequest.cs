using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class UpdateIgnoreTierChangeUntilRequest
    {
        public int TierId { get; set; }
        public DateTime? IgnoreTierChangeUntil { get; set; }
    }
}