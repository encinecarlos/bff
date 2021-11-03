using System;

namespace POC.Bff.Web.Domain.Dtos
{
    public class GeographicRegionDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}