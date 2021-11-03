using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Dtos
{
    public class SystemDetailDto
    {
        public decimal Price { get; set; }
        public decimal InsurancePrice { get; set; }
        public decimal Power { get; set; }
        public List<Guid> Items { get; set; }
        public bool IsCompleted { get; set; }
        public bool CanEdit { get; set; }
        public List<PvSystemListDto> PvSystemList { get; set; }
        public decimal? TotalPvSystemMarkup { get; set; }
        public decimal? TotalPvSystemCmv { get; set; }
        public decimal? TotalPvSystemTax { get; set; }
    }
}