using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class FindExternalAccountsRequest : BasePagedRequest
    {
        public string SearchTerm { get; set; }
        public List<int> AccountTypeList { get; set; }
        public List<int> AccountStatusList { get; set; }
        public Guid? OrganizationId { get; set; }
        public int Field { get; set; }
    }
}