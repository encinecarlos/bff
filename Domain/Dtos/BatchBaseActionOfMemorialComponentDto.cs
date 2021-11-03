using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Dtos
{
    public abstract class BatchBaseActionOfMemorialComponentDto
    {
        public List<GroupComponentCombinationDto> CombinationGroupList { get; set; }

        public List<ComponentCombinationDto> CombinationList { get; set; }

        public string Token { get; set; }

        public Guid? PromotionId { get; set; }
    }


}
