using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class CloneCouponRequest
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
