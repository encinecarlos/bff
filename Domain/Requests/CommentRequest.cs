using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class CommentRequest
    {
        public Guid ReferenceId { get; set; }
        public string Message { get; set; }
        public List<Guid> SicesUsersList { get; set; }
        public List<Guid> RolesList { get; set; }
        public List<Guid> CdList { get; set; }
    }
}