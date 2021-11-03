using POC.Shared.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Dtos
{
    public class UpdateOrganizationDto
    {
        public string LegalName { get; set; }

        public string TradeName { get; set; }

        public int PersonType { get; set; }

        public Document Document { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string ContactName { get; set; }

        public ErpCode ErpCustomerCode { get; set; }

        public object Tier { get; set; }

        public DateTime? ActivationDate { get; set; }

        public string PersistentUntil { get; set; }

        public string Timezone { get; set; }

        public bool Active { get; set; }

        public DateTime? CancellationDate { get; set; }

        public Guid? DistributionCenterId { get; set; }

        public string DistributionCenterName { get; set; }

        public Guid? SicesExpressDistributionCenterId { get; set; }

        public string CurrencyCode { get; set; }

        public Guid? ParentOrganizationId { get; set; }

        public IEnumerable<Guid> ChildrenOrganizations { get; set; }

        public Guid? AccountManagerUserId { get; set; }

        public IEnumerable<object> OtherDocuments { get; set; }

        public string CountryCode { get; set; }

        public string Language { get; set; }

        public string Currency { get; set; }
        public Address Address { get; set; }

        public bool InternalUser { get; set; }

        public string OldEmail { get; set; }

        public string OldContactName { get; set; }
    }
}