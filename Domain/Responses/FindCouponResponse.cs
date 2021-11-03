using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class FindCouponResponse
    {
        public int Total { get; set; }
        public List<CouponResponse> List { get; set; }
    }
}