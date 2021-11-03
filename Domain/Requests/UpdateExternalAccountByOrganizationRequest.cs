namespace POC.Bff.Web.Domain.Requests
{
    public class UpdateExternalAccountByOrganizationRequest
    {
        public string Email { get; set; }
        public string OldEmail { get; set; }
        public string ContactName { get; set; }
        public string OldContactName { get; set; }
    }
}