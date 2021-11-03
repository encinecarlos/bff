using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class FetchLoyaltyPointsResponse
    {
        public int Total { get; set; }
        public List<FetchLoyaltyPointsItemResponse> List { get; set; }

        public class FetchLoyaltyPointsItemResponse
        {
            public Guid Id { get; set; }
            public string Description { get; set; }
            public int Type { get; set; }
            public int Points { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
}