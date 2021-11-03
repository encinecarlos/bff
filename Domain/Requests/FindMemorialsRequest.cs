using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class FindMemorialsRequest : BasePagedRequest
    {
        public string Name { get; set; }

        public List<int> Status { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Field { get; set; }
    }
}