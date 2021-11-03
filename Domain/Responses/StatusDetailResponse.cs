using POC.Bff.Web.Domain.Dtos;
using System;

namespace POC.Bff.Web.Domain.Responses
{
    public class StatusDetailResponse
    {
        public Guid AccountId { get; set; }
        public int Status { get; set; }
        public CredentialDto UserInitials { get; set; }
    }
}