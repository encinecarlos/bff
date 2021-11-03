using System;
using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Requests
{
    public class ChangeNotesAsReadRequest
    {
        public List<Guid> NoteIdList { get; set; }
    }
}
