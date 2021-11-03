using System;

namespace POC.Bff.Web.Domain.Dtos
{
    public class BilletDto
    {
        public string Instructions { get; set; }
        public DateTime DueAt { get; set; }
        public string DocumentNumber { get; set; }
        public string Type { get; set; }
    }
}