using POC.Bff.Web.Domain.Requests;
using POC.Shared.Responses;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Loyalty
{
    public interface ILoyaltyService
    {
        Task<Response> FetchLoyaltyPoints(FetchLoyaltyPointsRequest filters);
    }
}