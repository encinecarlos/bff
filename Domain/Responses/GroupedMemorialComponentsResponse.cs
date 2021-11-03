using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class GroupedMemorialComponentsResponse
    {
        public string ErpCode { get; set; }

        public string Model { get; set; }

        public decimal CmvCombinationNumber => CdList.Count * TierList.Count;

        public List<Guid> CdList { get; set; } = new List<Guid>();

        public List<Guid> PowerList { get; set; } = new List<Guid>();

        public List<int> TierList { get; set; } = new List<int>();
    }
}