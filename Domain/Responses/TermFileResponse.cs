using System;

namespace POC.Bff.Web.Domain.Responses
{
    public class TermFileResponse
    {
        public string FileName { get; set; }
        public DateTime UploadedAt { get; set; }
        public string KeyName { get; set; }
        public string Url { get; set; }
        public string Number { get; set; }
    }
}