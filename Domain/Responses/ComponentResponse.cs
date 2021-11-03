using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class ComponentResponse
    {
        public string ErpCode { get; set; }

        public decimal Cmv { get; set; }

        public Guid CdId { get; set; }

        public List<object> PriceList { get; set; }

        public int Tier { get; set; }
    }
}