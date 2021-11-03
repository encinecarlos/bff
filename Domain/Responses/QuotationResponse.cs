using POC.Bff.Web.Domain.Fixed;
using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class QuotationResponse
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

        public List<Guid> TagIdList { get; set; }

        public bool IsExpress { get; set; }

        public decimal? IvaTaxValue { get; set; }

        public string Observation { get; set; }

        public object CustomerDetail { get; set; }

        public QuotationStatusResponse StatusDetail { get; set; }

        public PvSystemDetailResponse PvSystemDetail { get; set; }

        public InvoicingResponse InvoicingDetail { get; set; }

        public ShippingResponse ShippingDetail { get; set; }

        public PaymentResponse PaymentDetail { get; set; }

        public QuotationOrganizationDetailResponse OrganizationDetail { get; set; }

        public SaleResponse SaleFiscalDetail { get; set; }

        public TermFileResponse ProformaFile { get; set; }
    }
}