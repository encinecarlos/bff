using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public class PagedResponse<T>
    {
        public long Total { get; set; }
        public List<T> List { get; set; }

        public PagedResponse(long total, List<T> list)
        {
            Total = total;
            List = list;
        }
    }
}