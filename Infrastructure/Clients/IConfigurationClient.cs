using Refit;
using POC.Bff.Web.Domain.Requests;
using POC.Shared.Responses;
using System;
using System.Threading.Tasks;

namespace POC.Bff.Web.Infrastructure.Clients
{
    public interface IConfigurationClient
    {
        [Post("/api/v1/attachments")]
        Task<Response> CreateAttachment([Body] AttachmentRequest command);

        [Post("/api/v1/attachments/search")]
        Task<Response> FindAttachment([Body] GenerateUrlsAttachmentRequest command);

        [Get("/api/v1/bank/accounts")]
        Task<Response> FindBankAccounts();

        [Get("/api/v1/fdis")]
        Task<Response> FindFdis();

        [Get("/api/v1/tiers")]
        Task<Response> FindTiers([Query] FindLoyaltyTiersRequest request = null);

        [Get("/api/v1/phases")]
        Task<Response> FindPhases();

        [Get("/api/v1/powers")]
        Task<Response> FindPowers();

        [Get("/api/v1/powers/group")]
        Task<Response> FindGroupedPowers();

        [Get("/api/v1/powers/{id}")]
        Task<Response> GetPower(Guid id);

        [Get("/api/v1/states-codes/{countryCode}")]
        Task<Response> FindStatesCode(string countryCode);

        [Get("/api/v1/structure-varieties")]
        Task<Response> FindStructureVarieties();

        [Get("/api/v1/structure-varieties/distribution-center/{distributionCenterId}/tier/{tierId}/automatic-system/{automaticSystem}")]
        Task<Response> FindStructureVarietiesByDistributionCenterAndTier(Guid distributionCenterId, int tierId, bool automaticSystem);

        [Get("/api/v1/structure-varieties/{id}")]
        Task<Response> GetStructureVariety(Guid id);

        [Post("/api/v1/structure-varieties")]
        Task<Response> CreateStructureVariety([Body] StructureVarietyRequest request);

        [Get("/api/v1/voltages")]
        Task<Response> FindVoltages();

        [Get("/api/v1/voltages/{id}")]
        Task<Response> GetVoltage(Guid id);

        [Get("/api/v1/tiers/{id}")]
        Task<Response> GetLoyaltyTierById(int id);

        [Get("/api/v1/points")]
        Task<Response> FindLoyaltyPointsFromUser();

        [Post("/api/v1/attachments/delete")]
        Task<Response> DeleteAttachment([Body] DeleteOrganizationAttachmentRequest request);

        [Post("/api/v1/medias")]
        Task<Response> UploadMedia([Body] object command);

        [Patch("/api/v1/medias/{keyName}/check")]
        Task<Response> CheckMedia(string keyName);

        [Get("/api/v1/medias")]
        Task<Response> FindMedias();

        [Get("/api/v1/states-codes/validate")]
        Task<Response> ValidStateCode(string countryCode, string stateCode);

        #region CardsBrand
        [Post("/api/v1/cardsbrand/search")]
        Task<Response> FindAllCardsBrand();

        [Get("/api/v1/cardsbrand/{id}")]
        Task<Response> GetCardsBrandById(Guid id);

        [Post("/api/v1/cardsbrand")]
        Task<Response> AddCardsBrand([Body] CardsBrandRequest command);

        [Delete("/api/v1/cardsbrand/{id}")]
        Task<Response> DeleteCardsBrand(Guid id);

        [Put("/api/v1/cardsbrand/{id}")]
        Task<Response> UpdateCardsBrand(Guid id, [Body] CardsBrandRequest command);
        #endregion

        #region sicesExpressParameter
        [Get("/api/v1/parameters/sices-express")]
        Task<Response> GetAllParametersSicesExpress();

        [Put("/api/v1/parameters/sices-express/{id}")]
        Task<Response> PutSicesExpressParameters(Guid id, [Body] UpdateSicesExpressParameterRequest command);
        #endregion

        #region Coupon

        [Post("/api/v1/coupon")]
        Task<Response> AddNewCoupon(CouponRequest request);

        [Get("/api/v1/coupon/code")]
        Task<Response> GetCouponByCode(string code);

        [Post("/api/v1/coupon/clone")]
        Task<Response> CloneCoupon(CloneCouponRequest request);

        [Get("/api/v1/coupon")]
        Task<Response> GetCouponById(Guid id);

        [Put("/api/v1/coupon/{id}")]
        Task<Response> UpdateCoupon(Guid id, [Body] CouponRequest command);

        [Post("/api/v1/coupon/search")]
        Task<Response> FindAllCoupons([Body] FindAllCouponRequest query);

        [Delete("/api/v1/coupon/{id}")]
        Task<Response> DeleteCoupon(Guid id);

        [Get("/api/v1/parameters/coupon")]
        Task<Response> GetCouponParameters();

        #endregion

        [Get("/api/v1/shipping/rules")]
        Task<Response> FindRulesAsync();

        [Post("/api/v1/shipping/rules")]
        Task<Response> CreateRuleAsync();

        [Put("/api/v1/shipping/rules/{ruleId}")]
        Task<Response> UpdateRuleAsync(Guid ruleId, [Body] UpdateRuleRequest command);

        [Delete("/api/v1/shipping/rules/{ruleId}")]
        Task<Response> DeleteRuleAsync(Guid ruleId);

        [Patch("/api/v1/shipping/rules/{ruleId}/percent-range")]
        Task<Response> AddPercentRangeToRuleAsync(Guid ruleId, [Body] AddPercentRangeToRuleRequest command);

        [Delete("/api/v1/shipping/rules/{ruleId}/percent-range/{percentRangeId}")]
        Task<Response> RemovePercentRangeFromRuleAsync(Guid ruleId, Guid percentRangeId);

        [Patch("/api/v1/shippings/distribution-center/{distributionCenterId}")]
        Task<Response> BindRuleForRegionInDistributionCenterAsync(Guid distributionCenterId,
            [Body] AddNewRegionAndLinkToShippingRuleRequest command);

        [Delete("/api/v1/shippings/distribution-center/{distributionCenterId}/region/{regionId}")]
        Task<Response> DeleteRegionRuleInShippingRuleAsync(Guid distributionCenterId, Guid regionId);

        [Get("/api/v1/shipping/rules/{ruleId}")]
        Task<Response> FindRuleByIdAsync(Guid ruleId);

        [Get("/api/v1/shippings")]
        Task<Response> FindShippingRulesAsync();

        [Get("/api/v1/states")]
        Task<Response> FindStates();

        [Get("/api/v1/states-codes/culture")]
        Task<Response> GetCultureByCountryCode(string countryCode);

        [Post("/api/v1/geographic-regions/search")]
        Task<Response> FindGeographicRegionsByIdsAsync([Body] FindGeographicRegionsRequest request);

        [Get("/api/v1/shippings/distribution-center/{distributionCenterId}")]
        Task<Response> GetShippingRuleByDistributionCenterIdAsync(Guid distributionCenterId);

        [Post("/api/v1/shipping/rules/search")]
        Task<Response> FindRuleByIdsAsync([Body] FindRulesRequest request);

        [Get("/api/v1/shipping/rules/{ruleId}/systems/{totalSystemsPrice}/price")]
        Task<Response> CalculateShippingPrice(Guid ruleId, decimal totalSystemsPrice);

        [Get("/api/v1/tags")]
        Task<Response> FindTags();

        [Get("/api/v1/tags/{id}")]
        Task<Response> GetTagById(Guid id);

        [Post("/api/v1/tags")]
        Task<Response> CreateTag([Body] CreateTagRequest request);

        [Put("/api/v1/tags/{id}")]
        Task<Response> UpdateTag(Guid id, [Body] UpdateTagRequest request);

        [Delete("/api/v1/tags/{id}")]
        Task<Response> DeleteTag(Guid id);

        [Get("/api/v1/states-codes")]
        Task<Response> FindStatesCode();

        [Get("/api/v1/shippings/region/{regionId}")]
        Task<Response> GetRegionById(Guid regionId);
    }
}