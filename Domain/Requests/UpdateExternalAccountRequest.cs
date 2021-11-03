using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class UpdateExternalAccountRequest
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Type { get; set; }
    }
}