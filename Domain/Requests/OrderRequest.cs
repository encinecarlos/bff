using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Responses;
using POC.Shared.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public sealed class AddNewOrderRequest : OrderRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
    public sealed class UpdateOrderRequest : OrderRequest { }
    public class OrderRequest
    {
        public string OrderNumber { get; set; }
        public decimal Price { get; set; }
        public int PaymentMethod { get; set; }
        public string CouponCode { get; set; }
        public CouponResponse CouponDetail { get; set; }
        public List<ProductsOrderResponse> Products { get; set; }
        public Address ShippingAddress { get; set; }
        public OrderInvoicingDto InvoicingDetail { get; set; }
        public List<CreditCardDto> CreditCards { get; set; }
        public BilletDto Billet { get; set; }
        public CredentialDto CreatedBy { get; set; }
        public AccountResponse AccountOrder { get; set; }
        public OrderStatus StatusDetail { get; set; }
        public SicesExpressBaseRequest SicesExpressParameter { get; set; }
        public OrganizationResponse OrganizationParameters { get; set; }
        public CouponParameterResponse CouponParameter { get; set; }

        public class OrderStatus
        {
            public Guid AccountId { get; set; }
            public int Status { get; set; }
            public string UpdateAt { get; set; }
            public CredentialDto UserInitials { get; set; }
        }
    }
}