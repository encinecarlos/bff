using System;

namespace POC.Bff.Web.Domain.Dtos
{
    public class RegionRuleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid RuleId { get; set; }
        public Guid StateId { get; set; }
    }
}