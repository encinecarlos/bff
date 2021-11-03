using POC.Shared.Domain.Fixed;

namespace POC.Bff.Web.Domain.Requests
{
    public class BasePagedRequest
    {
        public int Take { get; set; } = 10;
        public int Skip { get; set; } = 0;
        public OrderType OrderType { get; set; } = OrderType.Ascending;
    }
}