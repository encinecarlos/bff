using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class ChangeMessageAsReadRequest
    {
        public List<Guid> MessageIdList { get; set; }
    }
}
