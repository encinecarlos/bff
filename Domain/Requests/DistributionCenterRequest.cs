using POC.Shared.Domain.ValueObjects;

namespace POC.Bff.Web.Domain.Requests
{
    public class DistributionCenterRequest
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public Document Document { get; set; }
        public string StateCode { get; set; }
        public int Type { get; set; }
        public string Timezone { get; set; }
        public string Abbreviation { get; set; }
        public string CountryCode { get; set; }
        public string Country { get; set; }
    }
}