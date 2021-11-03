using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Dtos
{
    public class FindMemorialDto
    {
        public Guid? Id { get; set; }

        public string Token { get; set; }

        public string SearchTerm { get; set; }

        public List<Guid> CdList { get; set; }

        public List<int> TierList { get; set; }

        public List<Guid> ManufacturerList { get; set; }

        public List<Guid> PowerList { get; set; }

        public List<int> ComponentTypeList { get; set; }
    }
}