using POC.Shared.Responses;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Authorizations
{
    public interface IAuthorizationService
    {
        Task<Response> GetAuthorizationDetail();
    }
}