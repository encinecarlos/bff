using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Responses;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class CouponProductsPriceRequest
    {
        public string CouponCode { get; set; }
        public CouponParameterResponse CouponParameter { get; set; }
        public CouponResponse CouponDetail { get; set; }
        public List<ProductPriceDto> Products { get; set; }
    }
}
