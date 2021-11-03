using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class SumShoppingCartResponse
    {
        public decimal Price { get; set; }
        public List<ProductsOrderResponse> Products { get; set; }
    }
}
