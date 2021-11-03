using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class FindGeographicRegionsRequest
    {
        public FindGeographicRegionsRequest(IEnumerable<Guid> geographicRegionIds)
        {
            GeographicRegionIds = geographicRegionIds;
        }

        public IEnumerable<Guid> GeographicRegionIds { get; set; }
    }
}