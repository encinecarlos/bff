using System;

namespace POC.Bff.Web.Domain.Dtos
{
    public class InsuranceDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public int Type { get; set; }

        public bool IsIncluded { get; set; }

        public decimal PvSystemPrice { get; set; }

        public decimal Value { get; set; }

        public string Description { get; set; }

        public string KeyName { get; set; }

        public decimal Price { get; set; }
    }
}