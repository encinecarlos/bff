using POC.Bff.Web.Domain.Fixed;
using POC.Bff.Web.Domain.Responses;
using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Dtos
{
    public class QuotationDto
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string QuotationNumber { get; set; }

        public decimal Price { get; set; }

        public DateTime? ExpirationAt { get; set; }

        public DateTime? ExpiredAt { get; set; }

        public DateTime? MinimumMemorialExpirationDate { get; set; }

        public DateTime? ProformaCreatedAt { get; set; }

        public string ProformaFileKeyName { get; set; }

        public bool CanSend { get; set; }

        public bool CanApprove { get; set; }

        public bool CanValidateQuotation { get; set; }

        public bool CanValidatePaymentReceipt { get; set; }

        public bool CanInitProduction { get; set; }

        public bool CanInitInvoicing { get; set; }

        public bool CanAddObservation { get; set; }

        public List<QuotationStatusType> CanStatusChangeList { get; set; }

        public int UnreadMessageCount { get; set; }

        public List<Guid> TagItems { get; set; }

        public bool IsExpress { get; set; }

        public decimal? IvaTaxValue { get; set; }

        public string Observation { get; set; }

        public object CustomerDetail { get; set; }

        public QuotationStatusDto StatusDetail { get; set; }

        public SystemDetailDto PvSystemDetail { get; set; }

        public InvoicingDto InvoicingDetail { get; set; }

        public ShippingDto ShippingDetail { get; set; }

        public PaymentDto PaymentDetail { get; set; }

        public string Pendency { get; set; }

        public QuotationOrganizationDetailResponse OrganizationDetail { get; set; }

        public SaleResponse SaleFiscalDetail { get; set; }

        public TermFileResponse ProformaFile { get; set; }
    }

    public class StatusDetailDto
    {
        public Guid AccountId { get; set; }
        public int Status { get; set; }
        public string UpdateAt { get; set; }
        public string UserInitials { get; set; }
    }
}