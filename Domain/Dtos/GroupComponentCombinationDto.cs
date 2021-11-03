using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Dtos
{
    public class GroupComponentCombinationDto
    {
        public string ErpCode { get; set; }

        public List<Guid> CdList { get; set; } = new List<Guid>();

        public List<int> TierList { get; set; } = new List<int>();

        public List<Guid> PowerList { get; set; } = new List<Guid>();
    }
}