using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class ValidateInternalAccountRequest
    {
        public List<Guid> Accounts { get; set; }
    }
}