using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Dtos
{
    public class ComponentCombinationDto
    {
        public string ErpCode { get; set; }

        public Guid? DistributionCenterId { get; set; }

        public int? TierId { get; set; }

        public List<Guid> PowerList { get; set; }
    }
}