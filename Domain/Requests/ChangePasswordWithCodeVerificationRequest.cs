namespace POC.Bff.Web.Domain.Requests
{
    public class ChangePasswordWithVerificationCodeRequest
    {
        public string Email { get; set; }
        public string VerificationCode { get; set; }
        public string Password { get; set; }
    }
}