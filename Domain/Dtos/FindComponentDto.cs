using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Dtos
{
    public class FindComponentDto
    {
        public string ErpCode { get; set; }

        public List<Guid> CdList { get; set; }

        public List<int> TierList { get; set; }

        public List<Guid> PowerList { get; set; }

        public List<Guid> ManufacturerList { get; set; }

        public List<int> ComponentTypeList { get; set; }

        public string SearchTerm { get; set; }

        public string Token { get; set; }
    }
}