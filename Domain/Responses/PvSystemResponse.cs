using POC.Bff.Web.Domain.Dtos;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class PvSystemResponse
    {
        public decimal? Price { get; set; }
        public decimal? InsurancePrice { get; set; }
        public decimal? Power { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? CanEdit { get; set; }
        public List<SystemDto> PvSystemList { get; set; }
    }
}