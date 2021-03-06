using POC.Bff.Web.Domain.Fixed;
using POC.Shared.Domain.Fixed;
using System;

namespace POC.Bff.Web.Domain.Dtos
{
    public class QuotationStatusDto
    {
        public QuotationStatusType Status { get; set; }
        public string UserInitials { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid AccountId { get; set; }
        public UserType UserType { get; set; }
    }
}