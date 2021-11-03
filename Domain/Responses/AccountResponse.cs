using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class AccountResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string AvatarUrlKey { get; set; }
        public string PhoneNumber { get; set; }
        public string CurrencyCode { get; set; }
        public string Timezone { get; set; }
        public string OrganizationId { get; set; }
        public bool IsInternalUser { get; set; }
        public virtual List<object> Sections { get; set; }
    }
}