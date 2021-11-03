using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class CreateFinancialReportRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<int> PaymentMethods { get; set; }
        public string SearchTerm { get; set; }
        public List<int> Tiers { get; set; }
        public List<int> Status { get; set; }
        public decimal PowerMin { get; set; }
        public decimal PowerMax { get; set; }
        public decimal PriceMin { get; set; }
        public decimal PriceMax { get; set; }
    }
}