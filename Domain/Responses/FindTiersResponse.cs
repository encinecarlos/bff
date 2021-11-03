using System;

namespace POC.Bff.Web.Domain.Responses
{
    public sealed class FindTiersResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public File Image { get; set; }
        public class File
        {
            public string FileName { get; set; }
            public DateTime? UploadedAt { get; set; }
            public string KeyName { get; set; }
        }
    }
}