using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class SystemRequest
    {
        public Guid? Id { get; set; }

        public Guid? SelectedPromotionId { get; set; }

        public string SelectedPromotionName { get; set; }

        public bool? AutoGenerated { get; set; }

        public decimal? Power { get; set; }

        public string PowerLabel { get; set; }

        public decimal? Price { get; set; }

        public decimal? InsurancePrice { get; set; }

        public object PvSystemParams { get; set; }

        public IEnumerable<object> Components { get; set; }

        public List<object> Insurances { get; set; }

        public decimal PvSystemMarkup { get; set; }

        public decimal PvSystemCmv { get; set; }

        public decimal PvSystemTax { get; set; }
    }
}