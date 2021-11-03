using POC.Bff.Web.Domain.Requests;
using POC.Shared.Responses;
using System;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.DistributionCenters
{
    public interface IDistributionCenterService
    {
        Task<Response> GetDistributionCenter(Guid id);
        Task<Response> FindDistributionCenters();
        Task<Response> CreateDistributionCenter(DistributionCenterRequest command);
        Task<Response> DeleteManufacturer(Guid id);
        Task<Response> UpdateDistributionCenter(Guid id, DistributionCenterRequest command);
        Task<Response> FindPagedDistributionCenters(FindPagedDistributionCentersQueryRequest request);
    }
}