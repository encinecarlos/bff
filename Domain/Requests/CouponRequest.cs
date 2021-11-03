using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Fixed;
using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class CouponRequest
    {
        public Guid Id { get; set; }
        public string CouponCode { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public CouponStatusType Status { get; set; }
        public bool ExpirationEnabled { get; set; }
        public DateTime? ExpirationAt { get; set; }
        public List<ContextTypes> Contexts { get; set; }
        public decimal DiscountValue { get; set; }
        public Guid? OrganizationId { get; set; }
        public string CurrencyCode { get; set; }
        public CredentialDto CreatedBy { get; set; }
        public Guid? QuotationId { get; set; }
        public Guid? OrderId { get; set; }
        public string CountryCode { get; set; }
        public CouponOrganizationDetail OrganizationDetail { get; set; }
        public bool OrganizationIsValid { get; set; } = true;
    }
}
