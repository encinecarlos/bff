using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class ManufacturerRequest
    {
        public object Name { get; set; }

        public List<object> BuiltComponents { get; set; }
    }
}