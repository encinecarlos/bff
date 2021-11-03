using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class FindPromotionsRequest : BasePagedRequest
    {
        public string SearchTerm { get; set; }
        public List<int> Status { get; set; }
        public int? Field { get; set; }
    }
}