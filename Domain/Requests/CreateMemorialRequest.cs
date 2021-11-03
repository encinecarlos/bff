using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class CreateMemorialRequest
    {
        public string Name { get; set; }
        public DateTime EstimatedExpirationAt { get; set; }
    }
}