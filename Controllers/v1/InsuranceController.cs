using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Services.v1.Organizations;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/insurances")]
    public class InsuranceController : BaseController
    {
        private readonly IOrganizationService _organizationService;

        public InsuranceController(
            ILogger<InsuranceController> logger,
            IResponseService responseService,
            IOrganizationService organizationService) : base(logger, responseService)
        {
            _organizationService = organizationService;
        }

        /// <summary>
        ///     Find All Insurances by filters
        /// </summary>
        /// <returns>List of insurances conditions</returns>
        [HttpGet("search")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindInsurances([FromQuery] FindInsuranceRequest query)
        {
            return await SafeExecuteAsync(async () => await _organizationService.FindInsurances(query), HttpMethod.Get);
        }

        /// <summary>
        ///     Get Insurances by ranges
        /// </summary>
        /// <returns>List of insurances that satisfy the specified ranges</returns>
        [HttpGet("ranges")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> GetInsuranceByRange([FromQuery] FindInsuranceByRangesRequest query)
        {
            return await SafeExecuteAsync(async () => await _organizationService.GetInsuranceByRange(query),
                HttpMethod.Get);
        }

        /// <summary>
        ///     Validate if a promotion gives free insurance and includes that insurance
        /// </summary>
        /// <param name="promotionId">Promotion Identifier</param>
        /// <returns>ID of included insurance</returns>
        [HttpPost("promotion-insurance/{promotionId}")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> ValidateFreeInsurancePromotion(Guid promotionId)
        {
            return await SafeExecuteAsync(async () => await _organizationService.GetInsuranceByRange(promotionId),
                HttpMethod.Post);
        }
    }
}