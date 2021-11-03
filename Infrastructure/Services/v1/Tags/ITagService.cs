using POC.Shared.Responses;
using System;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Tags
{
    public interface ITagService
    {
        Task<Response> DeleteTag(Guid id);
    }
}