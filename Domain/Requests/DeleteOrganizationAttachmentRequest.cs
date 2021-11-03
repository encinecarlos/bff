namespace POC.Bff.Web.Domain.Requests
{
    public class DeleteOrganizationAttachmentRequest
    {
        public string KeyName { get; set; }
        public bool IsPrivate { get; set; }
    }
}