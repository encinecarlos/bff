using POC.Shared.Domain.ValueObjects;

namespace POC.Bff.Web.Domain.Responses
{
    public class FindModulesBySearchTermResponse : FindComponentsBySearchTermResponse
    {
        public string Efficiency;
        public string CellType { get; set; }
        public Power Power { get; set; }
    }
}