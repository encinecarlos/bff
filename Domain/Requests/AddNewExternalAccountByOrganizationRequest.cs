using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class AddNewOwnerExternalAccountRequest
    {
        public Guid OrganizationId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Language { get; set; }
        public string CountryCode { get; set; }
        public string CurrencyCode { get; set; }
        public string Country { get; set; }
    }
}