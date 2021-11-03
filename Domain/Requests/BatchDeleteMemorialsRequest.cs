using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class BatchDeleteMemorialsRequest
    {
        public List<Guid> Memorials { get; set; }
    }
}