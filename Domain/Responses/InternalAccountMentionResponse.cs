using System;

namespace POC.Bff.Web.Domain.Responses
{
    public class InternalAccountMentionResponse
    {
        public Guid Id { get; set; }
        public Guid Organization { get; set; }
        public string RoleName { get; set; }
        public bool IsRole { get; set; }
    }
}
