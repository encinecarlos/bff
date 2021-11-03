using POC.Bff.Web.Domain.Requests;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Dtos
{
    public class PvSystemDto
    {
        public decimal? Price { get; set; }
        public decimal? InsurancePrice { get; set; }
        public decimal? Power { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? CanEdit { get; set; }
        public List<SystemRequest> PvSystems { get; set; }
    }
}