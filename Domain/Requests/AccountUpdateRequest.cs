using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class AccountUpdateRequest
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Language { get; set; }
        public string Timezone { get; set; }
        public string Country { get; set; }
        public string CurrencyCode { get; set; }
        public Guid OrganizationId { get; set; }
        public bool IsInternalUser { get; set; }
        public string AvatarUrlKey { get; set; }
        public string CountryCode { get; set; }
        public List<string> Roles { get; set; }
    }
}