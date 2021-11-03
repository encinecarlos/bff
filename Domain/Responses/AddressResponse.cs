namespace POC.Bff.Web.Domain.Responses
{
    public class AddressResponse
    {
        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Number { get; set; }
        public string StateCode { get; set; }
        public string Complement { get; set; }
        public string ZipCode { get; set; }
        public string CountryCode { get; set; }
        public bool IsCompleted { get; set; }
    }
}