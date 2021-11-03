namespace POC.Bff.Web.Domain.Requests
{
    public class ChangePasswordAccountRequest
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}