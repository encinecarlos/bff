using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public sealed class ComponentConfigurationCommercialRequest
    {
        public bool TierLinkedEnabled { get; set; }
        public bool HasCustomComponents { get; set; }
        public int SelectedTierId { get; set; }
        public List<ComponentGroupCommercialRequest> GroupedComponents { get; set; }
    }
}