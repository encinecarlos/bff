using POC.Shared.Domain.Fixed;

namespace POC.Bff.Web.Domain.Requests
{
    public class ComponentListBySearchTermRequest : BasePagedRequest
    {
        public string SearchTerm { get; set; }
        public ComponentType ComponentType { get; set; }
    }
}