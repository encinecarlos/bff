namespace POC.Bff.Web.Domain.Requests
{
    public sealed class AddressRequest
    {
        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Number { get; set; }
        public string StateCode { get; set; }
        public string Complement { get; set; }
        public string ZipCode { get; set; }
    }
}