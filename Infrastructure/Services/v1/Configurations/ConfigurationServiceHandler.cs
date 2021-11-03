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
using System.Linq;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Configurations
{
    public class ConfigurationServiceHandler : IConfigurationService
    {
        private readonly IComponentClient _componentClient;
        private readonly IConfigurationClient _configurationClient;
        private readonly IIdentityClient _identityClient;
        private readonly IMapper _mapper;
        private readonly IResponseService _responseService;

        public ConfigurationServiceHandler(
            IConfigurationClient configurationClient,
            IComponentClient componentClient,
            IIdentityClient identityClient,
            IMapper mapper,
            IResponseService responseService)
        {
            _configurationClient = configurationClient;
            _componentClient = componentClient;
            _identityClient = identityClient;
            _mapper = mapper;
            _responseService = responseService;
        }

        public async Task<Response> FindTiers(FindLoyaltyTiersRequest request = null)
        {
            var tiers = (await _configurationClient.FindTiers(request)).Parse
                <IList<FindTiersResponse>>();

            var imageTierUrls = (await _configurationClient.FindAttachment(new GenerateUrlsAttachmentRequest
            {
                IsPrivate = false,
                KeyNames = tiers.GetKeyNames()
            })).Parse<List<AttachmentResponse>>();


            return _responseService.CreateSuccessResponse(tiers.MergeAttachment(imageTierUrls));
        }

        public async Task<Response> FindPowers()
        {
            return await _configurationClient.FindPowers();
        }

        public async Task<Response> GetPower(Guid id)
        {
            return await _configurationClient.GetPower(id);
        }

        public async Task<Response> FindGroupedPowers()
        {
            return await _configurationClient.FindGroupedPowers();
        }

        public async Task<BffResponse> FindStatesCode(string countryCode)
        {
            return new BffResponse
            {
                Data = new
                {
                    stateList = (await _configurationClient.FindStatesCode(countryCode)).Data
                }
            };
        }

        public async Task<Response> FindStructureVarieties()
        {
            return await _configurationClient.FindStructureVarieties();
        }

        public async Task<Response> GetStructureVariety(Guid id)
        {
            return await _configurationClient.GetStructureVariety(id);
        }

        public async Task<Response> CreateStructureVariety(StructureVarietyRequest request)
        {
            return await _configurationClient.CreateStructureVariety(request);
        }

        public async Task<Response> FindVoltages()
        {
            return await _configurationClient.FindVoltages();
        }

        public async Task<Response> GetVoltage(Guid id)
        {
            return await _configurationClient.GetVoltage(id);
        }

        public async Task<BffResponse> FindStatesCodeTier(string countryCode)
        {
            var stateCodeResponse = await _configurationClient.FindStatesCode(countryCode);

            var tierResponse = await FindTiers(new FindLoyaltyTiersRequest(true));

            var stateList = stateCodeResponse.Parse<List<string>>();
            var tiers = tierResponse.Parse<List<TierDto>>();

            var notifications = new List<NotificationResponse>();

            notifications.AddRange(stateCodeResponse.Notifications);
            notifications.AddRange(tierResponse.Notifications);

            return new BffResponse
            {
                Data = new
                {
                    stateList,
                    tiers
                },
                Notifications = notifications.Select(x => new { componentGroupId = x.FieldName, x.Type, x.Notification }),
                Success = !notifications.Any()
            };
        }

        public async Task<BffResponse> FindStatesCodeDistributionCentersInternalUsers(string countryCode)
        {
            try
            {
                var request = new PermissionInternalAccountRequest { ModulesType = new List<int> { 2 }, PermissionType = 2 };

                var hasPermission = await _identityClient.VerifyInternalAccountPermission(request);

                if (!hasPermission.Success)
                    return _responseService.CreateFailResponse(null).AddNotifications(hasPermission.Notifications);

                var stateCodeResponse = _configurationClient.FindStatesCode(countryCode);
                var distributionCentersResponse = _componentClient.FindDistributionCenters();
                var usersResponse = _identityClient.FindAccounts(2);

                await Task.WhenAll(stateCodeResponse, distributionCentersResponse, usersResponse);

                var stateList = stateCodeResponse.Result.Parse<List<string>>();
                var cd = distributionCentersResponse.Result.Parse<List<DistributionCenterDto>>();
                var user = usersResponse.Result.Parse<List<AccountResponse>>();

                var notifications = new List<NotificationResponse>();

                notifications.AddRange(stateCodeResponse.Result.Notifications);
                notifications.AddRange(distributionCentersResponse.Result.Notifications);
                notifications.AddRange(usersResponse.Result.Notifications);

                var sicesUsersList = user.Select(x => _mapper.Map<GenericResponseDto>(x));
                var distributionCenterList = cd.Select(x => _mapper.Map<GenericResponseDto>(x));

                return new BffResponse
                {
                    Data = new
                    {
                        stateList,
                        sicesUsersList,
                        distributionCenterList
                    },
                    Notifications = notifications.Select(x => new { componentGroupId = x.FieldName, x.Type, x.Notification }),
                    Success = !notifications.Any()
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<BffResponse> FindStatesCodeTiersInternalUsers(string countryCode)
        {
            var request = new PermissionInternalAccountRequest { ModulesType = new List<int> { 1 }, PermissionType = 2 };

            var hasPermission = await _identityClient.VerifyInternalAccountPermission(request);

            if (hasPermission.Success is false)
            {
                return _responseService.CreateFailResponse().AddNotifications(hasPermission.Notifications);
            }

            var stateCodeResponse = await _configurationClient.FindStatesCode(countryCode);
            var tierResponse = await FindTiers();
            var usersResponse = await _identityClient.FindAccounts(1);

            if (stateCodeResponse.Data == null || tierResponse.Data == null || usersResponse.Data == null)
                return new BffResponse();

            var notifications = new List<NotificationResponse>();

            notifications.AddRange(stateCodeResponse.Notifications);
            notifications.AddRange(tierResponse.Notifications);
            notifications.AddRange(usersResponse.Notifications);


            var tiers = tierResponse.Parse<List<TierDto>>();
            var user = usersResponse.Parse<List<AccountResponse>>();

            var keynames = tiers.Select(x => x.KeyName);
            var findAttachments = new GenerateUrlsAttachmentRequest { KeyNames = keynames.ToList(), IsPrivate = true };
            var tierFiles = await _configurationClient.FindAttachment(findAttachments);

            var stateList = stateCodeResponse.Parse<List<string>>();
            var sicesUsersList = user.Select(x => _mapper.Map<GenericResponseDto>(x));
            var tierList = PopulateTierFiles(tiers, tierFiles);

            return new BffResponse
            {
                Data = new
                {
                    stateList,
                    tierList,
                    sicesUsersList
                },
                Notifications = notifications.Select(x => new { componentGroupId = x.FieldName, x.Type, x.Notification }),
                Success = !notifications.Any()
            };
        }

        public async Task<Response> FindShippingRuleConfiguration()
        {
            var distributionCentersResult = _componentClient.FindDistributionCenters();
            var rulesResult = _configurationClient.FindRulesAsync();
            var shippingRulesResult = _configurationClient.FindShippingRulesAsync();
            var statesResult = _configurationClient.FindStates();

            await Task.WhenAll(distributionCentersResult, rulesResult, shippingRulesResult, statesResult);


            var bindDistributionCenters = distributionCentersResult.Result
                .Parse<List<DistributionCenterDto>>()
                .Select(x => new
                {
                    x.Id,
                    Name = x.Description
                }).ToList();

            var bindRules = rulesResult.Result
                .Parse<List<RuleDto>>();

            var bindShippingRules = shippingRulesResult.Result
                .Parse<List<ShippingRuleDto>>()
                .Select(x => new
                {
                    x.Id,
                    x.DistributionCenterId,
                    x.Regions
                }).ToList();

            var bindStates = statesResult.Result
                .Parse<List<StateDto>>();


            var ruleConfigurationResponse = bindRules.Select(rule =>
            {
                return new
                {
                    rule.Id,
                    rule.Name,
                    rule.MinPrice,
                    rule.Markup,
                    PercentRangeList = rule.PercentRanges,
                    DistributionCenterList = bindShippingRules
                        .Where(s => s.Regions.Select(r => r.RuleId).Contains(rule.Id))
                        .Select(shippingRule => new
                        {
                            bindDistributionCenters?.Find(x => x.Id == shippingRule.DistributionCenterId)?.Name,
                            RegionList = shippingRule.Regions.Select(region => new
                            {
                                StateAbbreviation = bindStates?.Find(x => x.Id == region.StateId)?.Code,
                                RegionName = region.Name
                            })
                        })
                };
            });

            return _responseService.CreateSuccessResponse(new
            {
                DistributionCenterList = bindDistributionCenters,
                ShippingRuleList = ruleConfigurationResponse
            });
        }

        public async Task<Response> CreateRule()
        {
            var newRule = (await _configurationClient.CreateRuleAsync()).Parse<AddNewRuleResponse>();

            return _responseService.CreateSuccessResponse(new
            {
                newRule.Id,
                newRule.Name,
                newRule.UpdatedAt,
                PercentRangeList = newRule.PercentRanges,
                DistributionCenterList = new List<object>()
            });
        }

        public async Task<Response> UpdateRule(Guid ruleId, UpdateRuleRequest request)
        {
            return await _configurationClient.UpdateRuleAsync(ruleId, request);
        }

        public async Task<Response> DeleteRule(Guid ruleId)
        {
            return await _configurationClient.DeleteRuleAsync(ruleId);
        }

        public async Task<Response> AddPercentRangeToRule(Guid ruleId, AddPercentRangeToRuleRequest request)
        {
            return await _configurationClient.AddPercentRangeToRuleAsync(ruleId, request);
        }

        public async Task<Response> RemovePercentRangeFromRule(Guid ruleId, Guid percentRangeId)
        {
            return await _configurationClient.RemovePercentRangeFromRuleAsync(ruleId, percentRangeId);
        }

        public async Task<Response> BindRuleForRegionInDistributionCenter(Guid distributionCenterId, AddNewRegionAndLinkToShippingRuleRequest request)
        {
            var distributionCenterResponse = await _componentClient.GetDistributionCenter(distributionCenterId);
            if (distributionCenterResponse.Data is null)
                return _responseService.CreateFailResponse().AddNotifications(distributionCenterResponse.Notifications);

            return await _configurationClient.BindRuleForRegionInDistributionCenterAsync(distributionCenterId, request);
        }

        public async Task<Response> FindRegionsByDistributionCenter(Guid distributionCenterId)
        {
            var states = (await _configurationClient.FindStates()).Parse<List<StateDto>>();

            var geographicRegionIds = states?.Select(state => state.GeographicRegionId).Distinct();

            var regions =
                (await _configurationClient.FindGeographicRegionsByIdsAsync(
                    new FindGeographicRegionsRequest(geographicRegionIds)))
                .Parse<List<GeographicRegionDto>>();

            var distributionCenterRegions = states?.Select(state => state.GeographicRegionId).Distinct().Select(
                geographicRegionId =>
                {
                    return new RegionsByDistributionCenterResponse
                    {
                        Name = regions?.Find(x => x.Id == geographicRegionId).Description,
                        Abbreviation = regions?.Find(x => x.Id == geographicRegionId).Code,
                        StateList = states.Where(s => s.GeographicRegionId == geographicRegionId)
                            .Select(state =>
                                new RegionsByDistributionCenterResponse.StateRuleResponse
                                {
                                    Id = state.Id,
                                    Name = state.Name,
                                    Abbreviation = state.Code
                                })
                    };

                });

            var shippingRule = (await _configurationClient.GetShippingRuleByDistributionCenterIdAsync(distributionCenterId))
                .Parse<ShippingRuleDto>();

            var ruleIds = shippingRule?.Regions?.Select(region => region.RuleId).Distinct();

            var rules = ruleIds != null ? (await _configurationClient.FindRuleByIdsAsync(new FindRulesRequest(ruleIds)))
                .Parse<List<RuleDto>>() : null;

            if (rules is null) return _responseService.CreateSuccessResponse(distributionCenterRegions);

            distributionCenterRegions = distributionCenterRegions?.Select(geographicRegion =>
            {
                geographicRegion.StateList = geographicRegion.StateList.Select(state =>
                {
                    state.RegionList = from regionRule in shippingRule.Regions.Where(region => region.StateId == state.Id)
                                       select new RegionsByDistributionCenterResponse.StateRuleResponse.RegionRuleResponse
                                       {
                                           Id = regionRule.Id,
                                           RegionName = regionRule.Name,
                                           LinkedRuleId = rules.Find(rule => rule.Id == regionRule.RuleId)?.Id ?? Guid.Empty,
                                           LinkedRuleName = rules.Find(rule => rule.Id == regionRule.RuleId)?.Name
                                       };

                    return state;
                });

                return geographicRegion;
            });

            return _responseService.CreateSuccessResponse(distributionCenterRegions);
        }

        public async Task<Response> DeleteRegionRuleInShippingRule(Guid distributionCenterId, Guid regionId)
        {
            return await _configurationClient.DeleteRegionRuleInShippingRuleAsync(distributionCenterId, regionId);
        }

        private static List<TierDto> PopulateTierFiles(List<TierDto> tierList, Response tierFiles)
        {
            var tiers = tierFiles.Parse<List<AttachmentResponse>>()
                .Join(tierList, a => a.KeyName, t => t.KeyName,
                    (attachmentResponse, tierResponse) => new TierDto
                    {
                        Id = tierResponse.Id,
                        Name = tierResponse.Name,
                        FileName = tierResponse.FileName,
                        KeyName = tierResponse.KeyName,
                        ImageUrl = attachmentResponse.Url
                    }).ToList();
            var tiersWithoutKeyName = tierList.Where(x => x.KeyName == null).ToList();
            tiers.AddRange(tiersWithoutKeyName);
            return tiers;
        }
    }
}