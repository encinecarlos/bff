using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Dtos
{
    public class OrganizationDto
    {
        public Guid Id { get; set; }
        public string LegalName { get; set; }
        public string TradeName { get; set; }
        public object Permissions { get; set; }
        public object Document { get; set; }
        public int Status { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ContactName { get; set; }
        public string ErpCustomerCode { get; set; }
        public TierDto Tier { get; set; }
        public DateTime? ActivationAt { get; set; }
        public DateTime? RegistrationAt { get; set; }
        public string PersistentUntil { get; set; }
        public string Timezone { get; set; }
        public bool Active { get; set; }
        public DateTime? CancellationDate { get; set; }
        public Guid DistributionCenterId { get; set; }
        public Guid? SicesExpressDistributionCenterId { get; set; }
        public string CurrencyCode { get; set; }
        public Guid? ParentOrganizationId { get; set; }
        public int PersonType { get; set; }
        public List<Guid> ChildrenOrganizations { get; set; }
        public Guid AccountManagerUserId { get; set; }
        public List<object> OtherDocuments { get; set; }
        public object Address { get; set; }
        public List<object> ShippingCompanies { get; set; }
        public List<object> DocumentFiles { get; set; }
        public int LoyaltyPoints { get; set; }
        public bool HasPendingTermAcceptance { get; set; }
        public bool HasSeenOnboarding { get; set; }
    }
}