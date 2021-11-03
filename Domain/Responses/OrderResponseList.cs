using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class OrderResponseList
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public decimal Price { get; set; }
        public object CreatedBy { get; set; }
        public OrderOrganization OrganizationDetail { get; set; }
        public List<object> Products { get; set; }
        public int Status { get; set; }
        public object PaymentDetail { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public class OrderOrganization
        {
            public string LegalName { get; set; }
            public object Document { get; set; }
        }
    }
}