using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class PvSystemDetailResponse
    {
        public decimal PvSystemPrice { get; set; }
        public decimal InsurancePrice { get; set; }
        public decimal Power { get; set; }
        public bool IsCompleted { get; set; }
        public bool CanEdit { get; set; }
        public List<PvSystemListResponse> PvSystemList { get; set; }
        public decimal? TotalPvSystemMarkup { get; set; }
        public decimal? TotalPvSystemCmv { get; set; }
        public decimal? TotalPvSystemTax { get; set; }
    }
}