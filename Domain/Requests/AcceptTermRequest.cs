using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class AcceptTermRequest
    {
        public List<Guid> AcceptedTerms { get; set; }
    }
}