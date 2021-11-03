using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class MlpeRequest : ComponentBaseRequest
    {
        public List<object> ElectricalDetails { get; set; }
    }
}