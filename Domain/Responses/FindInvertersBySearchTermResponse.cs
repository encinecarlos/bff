using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class FindInvertersBySearchTermResponse : FindComponentsBySearchTermResponse
    {
        public List<object> ElectricalDetails { get; set; }
    }
}