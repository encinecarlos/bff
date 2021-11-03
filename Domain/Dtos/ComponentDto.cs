using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Dtos
{
    public class ComponentDto
    {
        public string ErpCode { get; set; }

        public decimal Cmv { get; set; }

        public Guid DistributionCenterId { get; set; }

        public List<object> Prices { get; set; }

        public int Tier { get; set; }
    }
}