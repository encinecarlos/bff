using AutoMapper;
using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Extensions;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Domain.Responses;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Bff.Web.Infrastructure.Services.v1.Authorizations;
using POC.Shared.Domain.Fixed;
using POC.Shared.Responses;
using POC.Shared.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Components
{
    public class ComponentServiceHandler : IComponentService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ICommercialClient _commercialClient;
        private readonly IComponentClient _componentClient;
        private readonly IConfigurationClient _configurationClient;
        private readonly IMapper _mapper;
        private readonly IResponseService _responseService;

        public ComponentServiceHandler(
            ICommercialClient commercialClient,
            IComponentClient componentClient,
            IAuthorizationService authorizationService,
            IMapper mapper,
            IConfigurationClient configurationClient,
            IResponseService responseService)
        {
            _commercialClient = commercialClient;
            _componentClient = componentClient;
            _authorizationService = authorizationService;
            _mapper = mapper;
            _configurationClient = configurationClient;
            _responseService = responseService;
        }


        public async Task<Response> FindPublishedMemorialComponentsGroupedByType(
            FindPublishedMemorialComponentsGroupedByTypeRequest request)
        {
            return await _commercialClient.FindPublishedMemorialComponentsGroupedByType(request);
        }

        public async Task<Response> GetDataSheet(string erpCode)
        {
            return await _componentClient.GetDataSheet(erpCode);
        }

        public async Task<Response> ValidateSystemPower(ValidateComponentRequest command)
        {
            return await _componentClient.ValidateSystemPower(command);
        }

        public async Task<Response> GetGroupedComponentsByTier(int tierId)
        {
            var components = (await _componentClient.FindComponentsByTier(tierId))?
                .Parse<List<GroupComponentsDto>>();
            return _responseService.CreateSuccessResponse(GroupComponentsDto.Create(components));
        }

        public async Task<Response> GetComponentsByTier(int tierId)
        {
            var components = (await _componentClient.FindComponentsByTier(tierId))?
                .Parse<List<TierComponentsDto>>();

            return _responseService.CreateSuccessResponse(TierComponentsDto.Create(components));
        }

        public async Task<Response> FindComponentsBySearchTermAsync(ComponentListBySearchTermRequest request)
        {
            var authorizationResponse = await _authorizationService.GetAuthorizationDetail();
            var userDetails = authorizationResponse.Parse<AuthorizationResponse>();

            var organizationResponse =
                await _commercialClient.GetOrganization(Guid.Parse(userDetails.Account.OrganizationId));
            var organization = organizationResponse.Parse<OrganizationResponse>();

            var requestComponent = _mapper.Map<ComponentListBySearchTermComponentRequest>(request);

            requestComponent.Tier = organization.Tier.Id ?? default;
            requestComponent.DistributionCenterId = organization.DistributionCenterId ?? default;

            object response;

            var componentResponse = await _componentClient.FindComponentsBySearchTerm(requestComponent);

            switch (request.ComponentType)
            {
                case ComponentType.Inverter:
                    response = await PopulateImageUrl<FindInvertersBySearchTermResponse>(componentResponse);
                    break;
                case ComponentType.Mlpe:
                    response = await PopulateImageUrl<FindMlpesBySearchTermResponse>(componentResponse);
                    break;
                case ComponentType.Module:
                    response = await PopulateImageUrl<FindModulesBySearchTermResponse>(componentResponse);
                    break;
                case ComponentType.StringBox:
                    response = await PopulateImageUrl<FindStringBoxesBySearchTermResponse>(componentResponse);
                    break;
                case ComponentType.Structure:
                    response = await PopulateImageUrl<FindStructuresBySearchTermResponse>(componentResponse);
                    break;
                case ComponentType.Variety:
                    response = await PopulateImageUrl<FindVarietiesBySearchTermResponse>(componentResponse);
                    break;
                default:
                    response = null;
                    break;
            }

            return _responseService.CreateSuccessResponse(response);
        }

        public async Task<Response> FindTiersAndDistributionCenters()
        {
            var distributionCenters = (await _componentClient.FindDistributionCentersComponent()).Parse
                <IList<DistributionCenterDto>>();

            var tiers = (await _configurationClient.FindTiers()).Parse
                <IList<FindTiersResponse>>();

            var imageTierUrls = (await _configurationClient.FindAttachment(new GenerateUrlsAttachmentRequest
            {
                IsPrivate = false,
                KeyNames = tiers.GetKeyNames()
            })).Parse<List<AttachmentResponse>>();

            return _responseService.CreateSuccessResponse(new
            {
                cdList = distributionCenters,
                tierList = tiers.MergeAttachment(imageTierUrls)
            });
        }

        private async Task<PagedResponse<T>> PopulateImageUrl<T>(Response response)
            where T : FindComponentsBySearchTermResponse
        {
            var components = response.Parse<PagedResponse<T>>();

            var imagesUrl = (await _configurationClient.FindAttachment(new GenerateUrlsAttachmentRequest
            {
                IsPrivate = false,
                KeyNames = components?.List?.Select(x => x.ImageKeyName)?.ToList()
            })).Parse<List<AttachmentResponse>>();

            components?.List?.ToList().PopulateImageUrl(imagesUrl);

            return components;
        }
    }
}