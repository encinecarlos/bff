using POC.Shared.Domain.Fixed;
using System;

namespace POC.Bff.Web.Domain.Dtos
{
    public class CredentialDto
    {
        public string Name { get; set; }
        public string NameInitials { get; set; }
        public Guid Id { get; set; }
        public UserType UserType { get; set; }
    }
}