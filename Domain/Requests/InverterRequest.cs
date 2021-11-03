using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class InverterRequest : ComponentBaseRequest
    {
        public List<Guid> InterchangeabilityIds { get; set; }

        public List<object> ElectricalDetails { get; set; }
    }
}