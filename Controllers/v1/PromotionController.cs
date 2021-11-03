using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Bff.Web.Infrastructure.Services.v1.Promotions;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/promotions")]
    public class PromotionController : BaseController
    {
        private readonly ICommercialClient _commercialService;
        private readonly IPromotionService _promotionService;

        public PromotionController(
            ILogger<PromotionController> logger, IResponseService responseService,
            IPromotionService promotionService,
            ICommercialClient commercialService) : base(logger, responseService)
        {
            _promotionService = promotionService;
            _commercialService = commercialService;
        }

        /// <summary>
        ///     Create a new Promotion
        /// </summary>
        /// <param name="request">Json containing the fields to create the Promotion</param>
        /// <returns>Promotion Created</returns>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PostPromotion(PromotionRequest request)
        {
            return await SafeExecuteAsync(async () => await _promotionService.AddNewPromotion(request),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Get promotion by identifier
        /// </summary>
        /// <param name="id">promotion identification</param>
        /// <returns>Promotion</returns>
        [HttpGet("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> GetPromotion(Guid id)
        {
            return await SafeExecuteAsync(async () => await _promotionService.GetPromotion(id), HttpMethod.Get);
        }

        /// <summary>
        ///     Find All promotion by filters
        /// </summary>
        /// <returns>List of promotion</returns>
        [HttpPost("search")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindPromotion(FindPromotionsRequest filters)
        {
            return await SafeExecuteAsync(async () => await _promotionService.FindPromotionList(filters),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Update Promotion by identifier
        /// </summary>
        /// <param name="id">Promotion Identifier</param>
        /// <param name="command">Json containing the fields to update the Promotion</param>
        /// <returns>Promotion Updated</returns>
        [HttpPut("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PutPromotion(Guid id, PromotionRequest command)
        {
            return await SafeExecuteAsync(async () => await _promotionService.UpdatePromotion(id, command),
                HttpMethod.Put);
        }

        /// <summary>
        ///     Delete Promotion by identifier
        /// </summary>
        /// <param name="id">Promotion Identifier</param>
        /// <returns>StatusDetail Code 200 if successfully deleted</returns>
        [HttpDelete("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> DeletePromotion(Guid id)
        {
            return await SafeExecuteAsync(async () => await _commercialService.DeletePromotion(id), HttpMethod.Delete);
        }

        /// <summary>
        ///     Clone promotion
        /// </summary>
        /// <param name="id">Request to clone</param>
        /// <returns></returns>
        [HttpPost("{id}/clone")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> ClonePromotion(Guid id)
        {
            return await SafeExecuteAsync(async () => await _promotionService.ClonePromotion(id), HttpMethod.Post);
        }

        /// <summary>
        ///     Get all available promotions from the logged user organization
        /// </summary>
        /// <returns>Promotions</returns>
        [HttpGet("available")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> GetAvailablePromotion([FromQuery] Guid QuotationId)
        {
            return await SafeExecuteAsync(async () => await _promotionService.GetAvailablePromotion(QuotationId),
                HttpMethod.Get);
        }

        /// <summary>
        ///     Gets available tiers, distribution centers, insurances and grouped components.
        /// </summary>
        /// <returns>List with tiers, distribution centers, insurances and grouped components.</returns>
        [HttpGet("detail-config")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> GetDetailConfig()
        {
            return await SafeExecuteAsync(async () => await _promotionService.GetDetailConfig(), HttpMethod.Get);
        }

        /// <summary>
        ///     Change promotion Status
        /// </summary>
        /// ///
        /// <param name="id">promotion identification</param>
        /// ///
        /// <param name="status">new status to the Promotion</param>
        /// <returns></returns>
        [HttpPatch("{id}/status/{status}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> ValidatePromotionStatus(Guid id, int status)
        {
            return await SafeExecuteAsync(async () => await _commercialService.ValidatePromotionStatus(id, status),
                HttpMethod.Patch);
        }
    }
}