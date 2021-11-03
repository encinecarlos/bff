using POC.Bff.Web.Domain.Requests;
using POC.Shared.Responses;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Components
{
    public interface IComponentService
    {
        Task<Response> FindPublishedMemorialComponentsGroupedByType(FindPublishedMemorialComponentsGroupedByTypeRequest request);
        Task<Response> GetDataSheet(string erpCode);
        Task<Response> ValidateSystemPower(ValidateComponentRequest command);
        Task<Response> GetGroupedComponentsByTier(int tierId);
        Task<Response> GetComponentsByTier(int tierId);
        Task<Response> FindComponentsBySearchTermAsync(ComponentListBySearchTermRequest request);
        Task<Response> FindTiersAndDistributionCenters();
    }
}