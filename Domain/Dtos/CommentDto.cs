using POC.Bff.Web.Domain.Responses;
using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Dtos
{
    public class CommentDto
    {
        public Guid ReferenceId { get; set; }
        public string Message { get; set; }
        public object InternalUsers { get; set; }
        public List<InternalAccountMentionResponse> Accounts { get; set; }
        public Dictionary<Guid, bool> AccountsNotification { get; set; }
        public Guid ParentId { get; set; }
    }
}
