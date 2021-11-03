using POC.Shared.Domain.ValueObjects;
using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class CustomerRequest
    {
        public string Name { get; set; }

        public string ContactPerson { get; set; }

        public string StateInscription { get; set; }

        public Address Address { get; set; }

        public string Phone { get; set; }

        public object Document { get; set; }

        public string Email { get; set; }

        public int PersonType { get; set; }

        public Guid QuotationId { get; set; }
    }
}