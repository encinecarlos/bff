using System;

namespace POC.Bff.Web.Domain.Requests
{
    public class ChangeNotificationAsReadRequest
    {
        public Guid Id { get; set; }
        public bool AsRead { get; set; }
    }
}
