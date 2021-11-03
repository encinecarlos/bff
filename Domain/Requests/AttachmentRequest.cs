namespace POC.Bff.Web.Domain.Requests
{
    public class AttachmentRequest : BaseAttachmentRequest
    {
        public string FileName { get; set; }
        public string Content { get; set; }
        public bool SaveInvoicing { get; set; }
    }
}