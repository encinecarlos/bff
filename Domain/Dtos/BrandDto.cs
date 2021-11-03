using System;

namespace POC.Bff.Web.Domain.Dtos
{
    public class BrandDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}