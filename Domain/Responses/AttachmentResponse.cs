using System;

namespace POC.Bff.Web.Domain.Responses
{
    public class AttachmentResponse
    {
        public string KeyName { get; set; }
        public string FileName { get; set; }
        public DateTime UploadedAt { get; set; }
        public string Url { get; set; }
        public string DownloadLink { get; set; }
    }
}