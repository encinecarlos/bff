using AutoMapper;
using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Shared.Responses;
using System;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.DistributionCenters
{
    public class DistributionCenterServiceHandler : IDistributionCenterService
    {
        private readonly IComponentClient _componentClient;
        private readonly IMapper _mapper;

        public DistributionCenterServiceHandler(IComponentClient componentClient, IMapper mapper)
        {
            _componentClient = componentClient;
            _mapper = mapper;
        }

        public async Task<Response> GetDistributionCenter(Guid id)
        {
            return await _componentClient.GetDistributionCenter(id);
        }

        public async Task<Response> FindDistributionCenters()
        {
            return await _componentClient.FindDistributionCenters();
        }

        public async Task<Response> CreateDistributionCenter(DistributionCenterRequest command)
        {
            return await _componentClient.CreateDistributionCenter(command);
        }

        public async Task<Response> DeleteManufacturer(Guid id)
        {
            return await _componentClient.DeleteManufacturer(id);
        }

        public async Task<Response> UpdateDistributionCenter(Guid id, DistributionCenterRequest command)
        {
            return await _componentClient.UpdateDistributionCenter(id, command);
        }

        public async Task<Response> FindPagedDistributionCenters(FindPagedDistributionCentersQueryRequest request)
        {
            var requestApi = _mapper.Map<FindPagedDistributionCentersDto>(request);

            return await _componentClient.FindPagedDistributionCenters(requestApi);
        }
    }
}