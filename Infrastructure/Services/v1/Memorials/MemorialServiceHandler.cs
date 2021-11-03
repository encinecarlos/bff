using AutoMapper;
using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Extensions;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Domain.Responses;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Shared.Responses;
using POC.Shared.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Memorials
{
    public class MemorialServiceHandler : IMemorialService
    {
        private readonly ICommercialClient _commercialClient;
        private readonly IComponentClient _componentClient;
        private readonly IConfigurationClient _configurationClient;
        private readonly IMapper _mapper;
        private readonly IResponseService _responseService;

        public MemorialServiceHandler(
            ICommercialClient commercialClient,
            IConfigurationClient configurationClient,
            IComponentClient componentClient,
            IMapper mapper,
            IResponseService responseService)
        {
            _commercialClient = commercialClient;
            _configurationClient = configurationClient;
            _componentClient = componentClient;
            _mapper = mapper;
            _responseService = responseService;
        }

        public async Task<Response> GetMemorialDetailConfig()
        {
            var timeout = _commercialClient.GetMemorialSession();
            var tierList = _configurationClient.FindTiers();
            var cdList = _componentClient.FindDistributionCenters();
            var power = _configurationClient.FindGroupedPowers();
            var manufacturerList = _componentClient.FindGroupedBuiltComponents();
            var promotionList = _commercialClient.GetActivePromotions();

            await Task.WhenAll(
                timeout,
                cdList,
                tierList,
                power,
                manufacturerList,
                promotionList);

            return _responseService.CreateSuccessResponse(new
            {
                sessionTimeout = timeout.Result.Data,
                filterConfig = new
                {
                    cdList = cdList.Result.Data,
                    tierList = tierList.Result.Data,
                    manufacturerList = manufacturerList.Result.Data,
                    powerList = power.Result.Data,
                    promotionList = promotionList.Result.Data
                }
            });
        }

        public async Task<Response> FindMemorialComponents(Guid id, FindComponentDto query)
        {
            var queryRequest = _mapper.Map<FindComponentRequest>(query);
            var response = await _commercialClient.FindMemorialComponents(id, queryRequest);
            var bindComponentResponse = response.Parse<FindMemorialComponentsRequest<ComponentDto>>();
            var componentList = _mapper.Map<List<ComponentResponse>>(bindComponentResponse.ComponentList);
            var componentResponse = new { bindComponentResponse.Token, componentList };

            return _responseService.CreateSuccessResponse(componentResponse).AddNotifications(response.Notifications);
        }

        public async Task<Response> FindMemorialGroupComponents(Guid id, FindMemorialDto query)
        {
            var queryRequest = _mapper.Map<GetMemorialRequest>(query);
            var response = await _commercialClient.FindMemorialGroupComponents(id, queryRequest);
            var bindGroupedMemorialComponentsResponse =
                response.Parse<FindMemorialComponentsRequest<GroupedMemorialComponentsDto>>();
            var componentsList =
                _mapper.Map<List<GroupedMemorialComponentsResponse>>(bindGroupedMemorialComponentsResponse
                    .ComponentList);
            var componentResponse = new { bindGroupedMemorialComponentsResponse.Token, ComponentList = componentsList };

            return _responseService.CreateSuccessResponse(componentResponse).AddNotifications(response.Notifications);
        }

        public async Task<Response> UpdateMemorialComponentCmv(Guid id, string erpCode,
            UpdateMemorialComponentCmvDto command)
        {
            var request = _mapper.Map<UpdateMemorialComponentCmvRequest>(command);

            var response = await _commercialClient.UpdateMemorialComponentCmv(id, erpCode, request);

            return _responseService.CreateSuccessResponse(response.Data).AddNotifications(response.Notifications);
        }

        public async Task<Response> BatchChangeMemorialComponentCmv(Guid id, BatchChangeMemorialComponentDto command)
        {
            var request = _mapper.Map<BatchChangeMemorialComponentRequest>(command);

            var response = await _commercialClient.BatchChangeMemorialComponentCmv(id, request);

            return _responseService.CreateSuccessResponse(response.Data).AddNotifications(response.Notifications);
        }

        public async Task<Response> UpdateMemorialComponentMarkup(Guid id, string erpCode,
            UpdateMemorialComponentMarkupDto command)
        {
            var request = _mapper.Map<UpdateMemorialComponentMarkupRequest>(command);

            var response = await _commercialClient.UpdateMemorialComponentMarkup(id, erpCode, request);

            return _responseService.CreateSuccessResponse(response.Data).AddNotifications(response.Notifications);
        }

        public async Task<Response> CreateMemorial(CreateMemorialRequest command)
        {
            return await _commercialClient.CreateMemorial(command);
        }

        public async Task<Response> FindMemorials(FindMemorialsRequest query)
        {
            return await _commercialClient.FindMemorials(query);
        }

        public async Task<Response> GetMemorial(Guid id)
        {
            return await _commercialClient.GetMemorial(id);
        }

        public async Task<Response> SaveMemorialComponents(Guid id, SaveMemorialRequest command)
        {
            return await _commercialClient.SaveMemorialComponents(id, command);
        }

        public async Task<Response> CloneMemorial(Guid id)
        {
            var clonedMemorial = (await _commercialClient.CloneMemorial(id)).Parse<CloneMemorialResponse>();

            await _commercialClient.CloneComponentsFromPublishedMemorial(clonedMemorial.Id);

            return _responseService.CreateSuccessResponse(clonedMemorial);
        }

        public async Task<Response> PublishMemorial(Guid id)
        {
            return await _commercialClient.PublishMemorial(id);
        }

        public async Task<Response> BatchDeleteMemorials(BatchDeleteMemorialsRequest command)
        {
            return await _commercialClient.BatchDeleteMemorials(command);
        }

        public async Task<Response> GetMemorialSession()
        {
            return await _commercialClient.GetMemorialSession();
        }

        public async Task<Response> ChangeMemorial(Guid id, ChangeMemorialRequest command)
        {
            return await _commercialClient.ChangeMemorial(id, command);
        }

        public async Task<Response> BatchChangeComponentMarkup(Guid id, BatchChangeComponentMarkupDto command)
        {
            var request = _mapper.Map<BatchChangeComponentMarkupRequest>(command);

            var response = await _commercialClient.BatchChangeComponentMarkup(id, request);

            return _responseService.CreateSuccessResponse(response.Data).AddNotifications(response.Notifications);
        }
    }
}