using POC.Shared.Domain.Fixed;
using POC.Shared.Domain.ValueObjects;

namespace POC.Bff.Web.Domain.Dtos
{
    public class OrderInvoicingDto
    {
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public Document Document { get; set; }
        public PersonType PersonType { get; set; }
        public string Email { get; set; }
        public string StateInscription { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
    }
}
