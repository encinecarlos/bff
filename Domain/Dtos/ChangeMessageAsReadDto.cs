using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Dtos
{
    public class ChangeMessageAsReadDto
    {
        public List<Guid> MessagesIdentifiers { get; set; }
    }
}
