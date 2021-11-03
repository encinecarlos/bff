using POC.Bff.Web.Domain.Requests;
using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class ComponentValidateResponse
    {
        public bool HasModule { get; set; }

        public bool HasInverter { get; set; }

        public decimal Power { get; set; }

        public decimal FdiSys { get; set; }

        public string PowerLabel { get; set; }

        public bool IsCompatible { get; set; }

        public bool IsMaxFdi { get; set; }

        public bool IsMinFdi { get; set; }

        public Guid DistributionCenterId { get; set; }

        public int TierId { get; set; }

        public List<ComponentValuesRequest> Components { get; set; }

        public Guid? PromotionId { get; set; }
    }
}