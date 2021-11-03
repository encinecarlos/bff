using POC.Bff.Web.Domain.Requests;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Dtos
{
    public class FindPagedDistributionCentersDto : BasePagedRequest
    {
        public string SearchTerm { get; set; }
        public int? Type { get; set; }
        public List<string> StateCodes { get; set; }
        public int? Field { get; set; }
    }
}