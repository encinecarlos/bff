using System.Collections.Generic;

namespace POC.Bff.Web.Domain.Responses
{
    public sealed class FindClonesMemorialResponse
    {
        public IList<CloneMemorialResponse> List { get; set; }
    }
}