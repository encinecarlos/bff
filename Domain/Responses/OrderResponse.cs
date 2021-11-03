using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Fixed;
using POC.Bff.Web.Domain.Requests;
using POC.Shared.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using CreditCardDto = POC.Bff.Web.Domain.Dtos.CreditCardDto;

namespace POC.Bff.Web.Domain.Responses
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethodType PaymentMethod { get; set; }
        public CouponResponse CouponDetail { get; set; }
        public List<ProductsOrderResponse> Products { get; set; }
        public Address ShippingAddress { get; set; }
        public OrderInvoicingDto InvoicingDetail { get; set; }
        public List<CreditCardDto> CreditCards { get; set; }
        public BilletDto Billet { get; set; }
        public CredentialDto CreatedBy { get; set; }
        public AccountResponse AccountOrder { get; set; }
        public StatusDetailResponse StatusDetail { get; set; }
        public SicesExpressBaseRequest SicesExpressParameter { get; set; }
        public OrganizationResponse OrganizationParameters { get; set; }
        public PaymentDetailResponse PaymentDetail { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal? IvaTaxValue { get; set; }
    }


}