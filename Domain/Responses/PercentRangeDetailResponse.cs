using System;

namespace POC.Bff.Web.Domain.Responses
{
    public class PercentRangeDetailResponse
    {
        public Guid Id { get; set; }
        public decimal Percent { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}