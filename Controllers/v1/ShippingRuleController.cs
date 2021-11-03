using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Services.v1.Configurations;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/shipping/rules")]
    public class ShippingRuleController : BaseController
    {
        private readonly IConfigurationService _configurationService;

        public ShippingRuleController(
            ILogger<ShippingRuleController> logger,
            IConfigurationService configurationService,
            IResponseService responseService) : base(logger, responseService)
        {
            _configurationService = configurationService;
        }

        /// <summary>
        ///     Create a new shipping rule
        /// </summary>
        /// <returns>Shipping rule created</returns>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> CreateRule()
        {
            return await SafeExecuteAsync(async () => await _configurationService.CreateRule(), HttpMethod.Post);
        }

        /// <summary>
        ///     Update a shipping rule by identifier
        /// </summary>
        /// <param name="ruleId">shipping rule identifier</param>
        /// <param name="request">JSON with shipping rule modifications</param>
        /// <returns>Rule Updated</returns>
        [HttpPut("{ruleId}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> UpdateRule(Guid ruleId, UpdateRuleRequest request)
        {
            return await SafeExecuteAsync(async () => await _configurationService.UpdateRule(ruleId, request),
                HttpMethod.Put);
        }

        /// <summary>
        ///     Delete a identified shipping rule
        /// </summary>
        /// <param name="ruleId">Shipping rule identifier</param>
        [HttpDelete("{ruleId}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> DeleteRule(Guid ruleId)
        {
            return await SafeExecuteAsync(async () => await _configurationService.DeleteRule(ruleId),
                HttpMethod.Delete);
        }

        /// <summary>
        ///     Attach a percent range into a shipping rule
        /// </summary>
        /// <param name="ruleId">Shipping rule identifier</param>
        /// <param name="request">Requested percent range to attach</param>
        [HttpPatch("{ruleId}/percent-range")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> AddPercentRangeToRule(Guid ruleId, AddPercentRangeToRuleRequest request)
        {
            return await SafeExecuteAsync(
                async () => await _configurationService.AddPercentRangeToRule(ruleId, request), HttpMethod.Patch);
        }

        /// <summary>
        ///     Remove a percent range from a shipping rule
        /// </summary>
        /// <param name="ruleId">Shipping rule identifier</param>
        /// <param name="percentRangeId">Percent range identifier</param>
        [HttpDelete("{ruleId}/percent-range/{percentRangeId}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> RemovePercentRangeFromRule(Guid ruleId, Guid percentRangeId)
        {
            return await SafeExecuteAsync(
                async () => await _configurationService.RemovePercentRangeFromRule(ruleId, percentRangeId),
                HttpMethod.Delete);
        }

        /// <summary>
        ///     Find all distribution centers and rules for configuration
        /// </summary>
        /// <returns>List of distributions centers and rules</returns>
        [HttpGet("setup/configuration")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindShippingRuleConfiguration()
        {
            return await SafeExecuteAsync(async () => await _configurationService.FindShippingRuleConfiguration(),
                HttpMethod.Get);
        }
    }
}