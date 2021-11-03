using System;

namespace POC.Bff.Web.Domain.Dtos
{
    public class CreditCardDto
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public decimal PaymentAmount { get; set; }
        public int Installments { get; set; }

        public decimal InstallmentValue { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV { get; set; }
        public Guid BrandId { get; set; }
        public BrandDto BrandDetail { get; set; } = new BrandDto();
    }
}