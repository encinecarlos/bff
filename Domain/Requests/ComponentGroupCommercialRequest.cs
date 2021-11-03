using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public sealed class ComponentGroupCommercialRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ComponentOptionRequest> Options { get; set; }
    }
}