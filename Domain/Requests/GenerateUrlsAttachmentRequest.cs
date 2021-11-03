using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class GenerateUrlsAttachmentRequest : BaseAttachmentRequest
    {
        public List<string> KeyNames { get; set; }
    }
}