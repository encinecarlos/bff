using System;

namespace POC.Bff.Web.Domain.Dtos
{
    public class StateDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public Guid GeographicRegionId { get; set; }
    }
}