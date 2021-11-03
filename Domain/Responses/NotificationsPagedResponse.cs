using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class NotificationsPagedResponse
    {
        public Guid Id { get; set; }
        public bool IsRead { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid Account { get; set; }
        public string ProfileImgUrl { get; set; }
        public string Message { get; set; }
        public List<string> Links { get; set; }
        public int Section { get; set; }
    }
}
