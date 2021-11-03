namespace POC.Bff.Web.Domain.Requests
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string CountryCode { get; set; }
        public string Timezone { get; set; }
    }
}