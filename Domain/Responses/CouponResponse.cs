using POC.Bff.Web.Domain.Fixed;
using POC.Shared.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class CouponResponse
    {
        public Guid Id { get; set; }
        public string CouponCode { get; set; }
        public string Name { get; set; }
        public CouponStatusType Status { get; set; }
        public DateTime? ExpirationAt { get; set; }
        public bool ExpirationEnabled { get; set; }
        public Guid? OrganizationId { get; set; }
        public List<ContextTypes> Contexts { get; set; }
        public decimal DiscountValue { get; set; }
        public string CountryCode { get; set; }
        public Organization OrganizationDetail { get; set; }
    }

    public class Organization
    {
        public Guid Id { get; set; }
        public string TradeName { get; set; }
        public Document Document { get; set; }
    }
}