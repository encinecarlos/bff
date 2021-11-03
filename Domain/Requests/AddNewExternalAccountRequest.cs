using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class AddNewExternalAccountRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public int? Tier { get; set; }
        public Guid? OrganizationId { get; set; }
        public int Type { get; set; }
    }
}