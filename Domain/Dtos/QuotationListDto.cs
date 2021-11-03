using POC.Bff.Web.Domain.Responses;
using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Dtos
{
    public class QuotationListDto
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string QuotationNumber { get; set; }

        public DateTime? ExpirationAt { get; set; }

        public object CreatedBy { get; set; }

        public object OrganizationDetail { get; set; }

        public decimal Price { get; set; }

        public string ProformaFileKeyName { get; set; }

        public object PvSystemDetail { get; set; }

        public object PaymentDetail { get; set; }

        public object CustomerResumeDetail { get; set; }

        public bool IsImportant { get; set; }

        public string Pendency { get; set; }

        public int? Status { get; set; }

        public TermFileResponse ProformaFile { get; set; }

        public bool IsExpress { get; set; }

        public List<Guid> TagIdList { get; set; }
    }
}