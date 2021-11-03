using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class FindPagedDistributionCentersQueryRequest : BasePagedRequest
    {
        public string SearchTerm { get; set; }
        public int? Type { get; set; }
        public List<string> StateCodeList { get; set; }
        public int? Field { get; set; }
    }
}