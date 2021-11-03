using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class FindOrderResponse
    {
        public int Total { get; set; }
        public List<OrderResponseList> List { get; set; }
    }
}