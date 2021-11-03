using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class PermissionInternalAccountRequest
    {
        public List<int> ModulesType { get; set; }
        public int PermissionType { get; set; }
    }
}