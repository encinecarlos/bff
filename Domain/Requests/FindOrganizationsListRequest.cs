using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class FindOrganizationsListRequest : BasePagedRequest
    {
        public string SearchTerm { get; set; }
        public List<int> TierList { get; set; } = new List<int>();
        public List<string> StateCodeList { get; set; }
        public int Field { get; set; }
        public List<int> StatusList { get; set; }
        public Guid SicesUserId { get; set; }
    }
}