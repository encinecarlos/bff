using System;

namespace POC.Bff.Web.Domain.Responses
{
    public class LoginResponse
    {
        public string Email { get; set; }
        public bool IsInternalUser { get; set; }
        public string Token { get; set; }
        public DateTime NotBefore { get; set; }
        public DateTime Expires { get; set; }
        public bool IsOwner { get; set; }
        public Guid OrganizationId { get; set; }
    }
}