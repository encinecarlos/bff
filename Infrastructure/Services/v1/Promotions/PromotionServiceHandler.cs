using AutoMapper;
using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Extensions;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Domain.Responses;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Bff.Web.Infrastructure.Services.v1.Configurations;
using POC.Shared.Responses;
using POC.Shared.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Promotions
{
    public sealed class PromotionServiceHandler : IPromotionService
    {
        private readonly ICommercialClient _commercialClient;
        private readonly IComponentClient _componentClient;
        private readonly IConfigurationService _configurationService;
        private readonly IMapper _mapper;
        private readonly IResponseService _responseService;

        public PromotionServiceHandler(
            ICommercialClient commercialClient,
            IComponentClient componentClient,
            IConfigurationService configurationService,
            IMapper mapper,
            IResponseService responseService)
        {
            _commercialClient = commercialClient;
            _componentClient = componentClient;
            _configurationService = configurationService;
            _mapper = mapper;
            _responseService = responseService;
        }

        public async Task<Response> GetDetailConfig()
        {
            var tiersResult = _configurationService.FindTiers();
            var distributionCentersResult = _componentClient.FindDistributionCenters();
            var insurancesResult = _commercialClient.FindInsurances(new FindInsuranceRequest());
            var componentsResult = _componentClient.FindComponents();

            await Task.WhenAll(
                tiersResult,
                distributionCentersResult,
                insurancesResult,
                componentsResult);

            var tiers = tiersResult.Result.Parse<List<TierDto>>();

            var distributionCenters = distributionCentersResult.ParseDistributionCenters();

            var insurances = insurancesResult.ParseInsurances();

            var groupedComponents =
                GroupComponentsDto.Create(componentsResult.Result.Parse<List<GroupComponentsDto>>());

            return _responseService.CreateSuccessResponse(new
            {
                TierList = tiers,
                CdList = distributionCenters,
                InsuranceList = insurances,
                ComponentGroupList = groupedComponents
            });
        }

        public async Task<Response> AddNewPromotion(PromotionRequest request)
        {
            var publishedMemorialId = (await _commercialClient.GetPublishedMemorial()).Parse<Guid>();
            var clonedMemorial = (await _commercialClient.ClonePublishedMemorial()).Parse<CloneMemorialResponse>();

            await _commercialClient.CloneComponentsFromMemorial(publishedMemorialId, clonedMemorial.Id,
                request.GetAllErpCodes());

            var promotionCommercialRequest = _mapper.Map<PromotionRequest, PromotionCommercialRequest>(request);
            promotionCommercialRequest.MemorialId = clonedMemorial.Id;

            return await _commercialClient.AddNewPromotion(promotionCommercialRequest);
        }

        public async Task<Response> FindPromotionList(FindPromotionsRequest request)
        {
            await _commercialClient.ValidatePromotionStatus();

            var tiers = (await _configurationService.FindTiers()).Parse<List<TierDto>>();

            var searchResponse = (await _commercialClient.FindPromotions(request)).Parse
                <FindPromotionResponse>();

            if (searchResponse.List != null)
                searchResponse.List = searchResponse.List.Select(p =>
                {
                    p.TierList = p.TierList.Join(tiers, c => c.Id, k => k.Id, (c, k) =>
                    {
                        c.Name = k.Name;
                        c.Color = k.Color;
                        return c;
                    });
                    return p;
                });

            return _responseService.CreateSuccessResponse(searchResponse);
        }

        public async Task<Response> UpdatePromotion(Guid id, PromotionRequest request)
        {
            var publishedMemorialId = (await _commercialClient.GetPublishedMemorial()).Parse<Guid>();
            var currentPromotion = await GetPromotion<PromotionRequest>(id);

            var componentsToAdd = request.SubtractErpCodes(currentPromotion.GetAllErpCodes());
            var componentsToRemove = currentPromotion.SubtractErpCodes(request.GetAllErpCodes());

            var keepComponents = new List<Task>();

            if (componentsToAdd.Any())
                keepComponents.Add(_commercialClient.CloneComponentsFromMemorial(publishedMemorialId,
                    currentPromotion.MemorialId, componentsToAdd));

            if (componentsToRemove.Any())
                keepComponents.Add(
                    _commercialClient.DeleteComponentsByMemorial(currentPromotion.MemorialId, componentsToRemove));

            await Task.WhenAll(keepComponents.ToArray());

            var promotionCommercialRequest = _mapper.Map<PromotionRequest, PromotionCommercialRequest>(request);

            promotionCommercialRequest.MemorialId = currentPromotion.MemorialId;

            return await _commercialClient.UpdatePromotion(id, promotionCommercialRequest);
        }

        public async Task<Response> GetPromotion(Guid id)
        {
            var promotion = await GetPromotion<PromotionResponse>(id);

            return _responseService.CreateSuccessResponse(promotion);
        }

        public async Task<Response> GetAvailablePromotion(Guid id)
        {
            var promotions = (await _commercialClient.GetAvailablePromotions(id)).Parse<List<AvailablePromotionCommercialResponse>>();
            var response = promotions.Select(promotion => _mapper.Map<AvailablePromotionResponse>(promotion));

            return _responseService.CreateSuccessResponse(response);
        }

        public async Task<Response> ClonePromotion(Guid promotionId)
        {
            var promotion = await GetPromotion<PromotionRequest>(promotionId);

            if (promotion is null) return _responseService.CreateFailResponse();

            var clonedMemorial = (await _commercialClient.CloneMemorial(promotion.MemorialId)).Parse<CloneMemorialResponse>();

            await _commercialClient.CloneComponentsFromMemorial(promotion.MemorialId, clonedMemorial.Id, promotion.GetAllErpCodes());

            return await _commercialClient.ClonePromotion(promotionId, new ClonePromotionRequest(clonedMemorial.Id));
        }

        private async Task<T> GetPromotion<T>(Guid id)
        {
            var promotion = (await _commercialClient.GetPromotion(id)).Parse<GetPromotionByIdCommercialResponse>();

            var promotionResponse = _mapper.Map<GetPromotionByIdCommercialResponse, T>(promotion);

            return promotionResponse;
        }
    }
}