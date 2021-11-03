using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class FindMemorialComponentsRequest<T>
    {
        public string Token { get; set; }
        public List<T> ComponentList { get; set; }
    }
}