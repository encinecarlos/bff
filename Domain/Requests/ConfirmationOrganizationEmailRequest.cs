namespace POC.Bff.Web.Domain.Requests
{
    public class ConfirmationOrganizationEmailRequest
    {
        public string Email { get; set; }
        public string VerificationCode { get; set; }
    }
}