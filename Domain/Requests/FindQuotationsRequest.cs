using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class FindQuotationsRequest : BasePagedRequest
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string SearchTerm { get; set; }

        public int? TierId { get; set; }

        public List<int> Tiers { get; set; }

        public decimal? PowerMin { get; set; }

        public decimal? PowerMax { get; set; }

        public decimal? PriceMin { get; set; }

        public decimal? PriceMax { get; set; }

        public bool? OnlyImportant { get; set; }

        public List<int> Status { get; set; }

        public List<int> PaymentMethod { get; set; }

        public int? Field { get; set; }

        public string Organization { get; set; }

        public string Account { get; set; }

        public string CustomerId { get; set; }

        public Guid TagId { get; set; }

        public string OrderNumber { get; set; }

        public bool IsExpress { get; set; }

        public List<Guid> DistributionCenterIds { get; set; }

        public List<string> ErpCodes { get; set; }
    }
}