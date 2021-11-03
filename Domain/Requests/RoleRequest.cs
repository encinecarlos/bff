using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class RoleRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Dictionary<string, object> Permissions { get; set; }
    }
}