using POC.Shared.Responses;
using System.Collections.Generic;
using System.Linq;

namespace POC.Bff.Web.Infrastructure
{
    public class BffResponse
    {
        public object Data { get; set; }
        public IEnumerable<object> Notifications { get; set; }
        public bool Success { get; set; }


        public static implicit operator BffResponse(Response response)
        {
            if (response is null) return new BffResponse();

            return new BffResponse
            {
                Data = response.Data,
                Notifications = response.Notifications.Select(x => new { x.FieldName, x.Notification, x.Section, x.Type }),
                Success = response.Success
            };
        }
    }
}