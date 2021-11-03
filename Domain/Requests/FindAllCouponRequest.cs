using POC.Bff.Web.Domain.Fixed;
using POC.Shared.Domain.Models;

namespace POC.Bff.Web.Domain.Requests
{
    public class FindAllCouponRequest : BasePagedQuery
    {
        public string SearchTerm { get; set; }
        public CouponStatusType? Status { get; set; }
        public int? Field { get; set; }
    }
}
