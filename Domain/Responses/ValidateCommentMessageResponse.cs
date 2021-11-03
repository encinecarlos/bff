using System;

namespace POC.Bff.Web.Domain.Responses
{
    public class ValidateCommentMessageResponse
    {
        public Guid CreatedBy { get; set; }
        public Guid Organization { get; set; }
        public Guid AccountManager { get; set; }
        public bool InternalUser { get; set; }
    }
}
