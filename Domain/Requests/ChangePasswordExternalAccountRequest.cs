namespace POC.Bff.Web.Domain.Requests
{
    public class ChangePasswordExternalAccountRequest
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}