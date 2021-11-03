namespace POC.Bff.Web.Domain.Requests
{
    public class EmailConfirmationRequest
    {
        public string Email { get; set; }
        public string VerificationCode { get; set; }
    }
}