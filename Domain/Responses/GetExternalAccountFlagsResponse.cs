namespace POC.Bff.Web.Domain.Responses
{
    public class GetExternalAccountFlagsResponse
    {
        public bool EmailVerified { get; set; }
        public bool PasswordDefined { get; set; }
    }
}