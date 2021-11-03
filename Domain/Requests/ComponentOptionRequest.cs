using POC.Shared.Domain.Fixed;

namespace POC.Bff.Web.Domain.Requests
{
    public sealed class ComponentOptionRequest
    {
        public ComponentType Type { get; set; }
        public string ErpCode { get; set; }
        public string Model { get; set; }
    }
}