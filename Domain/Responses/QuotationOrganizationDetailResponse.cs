using POC.Shared.Domain.Fixed;
using POC.Shared.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class QuotationOrganizationDetailResponse
    {
        public Guid Id { get; set; }

        public string LegalName { get; set; }

        public string TradeName { get; set; }

        public Document Document { get; set; }

        public LoyaltyTierDetailResponse Tier { get; set; }

        public Guid? DistributionCenterId { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string StateInscription { get; set; }

        public string ContactName { get; set; }

        public ErpCode ErpCustomerCode { get; set; }

        public DateTime? ActivationDate { get; set; }

        public string PersistentUntil { get; set; }

        public string Timezone { get; set; }

        public DateTime? CancellationDate { get; set; }

        public string DistributionCenterName { get; set; }

        public Guid? SicesExpressDistributionCenterId { get; set; }

        public Guid? ParentOrganizationId { get; set; }

        public PersonType PersonType { get; set; }

        public IReadOnlyCollection<Guid> ChildrenOrganizations { get; set; }

        public Guid? AccountManagerUserId { get; set; }

        public IReadOnlyCollection<Document> OtherDocuments { get; set; }

        public Address Address { get; set; }

        public int LoyaltyBalance { get; set; }
        public string AccountManagerUserName { get; set; }
    }
}