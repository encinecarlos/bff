using POC.Bff.Web.Domain.Dtos;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class ProductsPriceRequest
    {
        public List<ProductPriceDto> Products { get; set; }
    }
}