using POC.Bff.Web.Domain.Requests;
using POC.Shared.Responses;
using System;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Services.v1.Configurations
{
    public interface IConfigurationService
    {
        Task<Response> FindTiers(FindLoyaltyTiersRequest request = null);
        Task<Response> FindPowers();
        Task<Response> GetPower(Guid id);
        Task<Response> FindGroupedPowers();
        Task<BffResponse> FindStatesCode(string countryCode);
        Task<Response> FindStructureVarieties();
        Task<Response> GetStructureVariety(Guid id);
        Task<Response> CreateStructureVariety(StructureVarietyRequest request);
        Task<Response> FindVoltages();
        Task<Response> GetVoltage(Guid id);
        Task<BffResponse> FindStatesCodeTier(string countryCode);
        Task<BffResponse> FindStatesCodeDistributionCentersInternalUsers(string countryCode);
        Task<BffResponse> FindStatesCodeTiersInternalUsers(string countryCode);
        Task<Response> FindShippingRuleConfiguration();
        Task<Response> CreateRule();
        Task<Response> UpdateRule(Guid ruleId, UpdateRuleRequest request);
        Task<Response> DeleteRule(Guid ruleId);
        Task<Response> AddPercentRangeToRule(Guid ruleId, AddPercentRangeToRuleRequest request);
        Task<Response> RemovePercentRangeFromRule(Guid ruleId, Guid percentRangeId);
        Task<Response> BindRuleForRegionInDistributionCenter(Guid distributionCenterId, AddNewRegionAndLinkToShippingRuleRequest request);
        Task<Response> FindRegionsByDistributionCenter(Guid distributionCenterId);
        Task<Response> DeleteRegionRuleInShippingRule(Guid distributionCenterId, Guid regionId);
    }
}