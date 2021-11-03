using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class FindOrdersRequest : BasePagedRequest
    {
        public string SearchTerm { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<int> PaymentMethod { get; set; }
        public List<int> Status { get; set; }
        public List<int> Types { get; set; }
        public decimal? PowerMin { get; set; }
        public decimal? PowerMax { get; set; }
        public string Field { get; set; }
        public decimal Price { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}