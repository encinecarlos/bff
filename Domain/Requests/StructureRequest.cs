using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class StructureRequest : ComponentBaseRequest
    {
        public object Dimension { get; set; }

        public List<int> Types { get; set; }
    }
}