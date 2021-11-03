using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Bff.Web.Infrastructure.Services.v1.Loyalty;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/loyalty")]
    public class LoyaltyController : BaseController
    {
        private readonly ICommercialClient _commercialClient;
        private readonly ILoyaltyService _loyaltyService;

        public LoyaltyController(
            ILogger<AuthorizationController> logger,
            IResponseService responseService,
            ILoyaltyService loyaltyService,
            ICommercialClient commercialClient) : base(logger, responseService)
        {
            _loyaltyService = loyaltyService;
            _commercialClient = commercialClient;
        }

        /// <summary>
        ///     Fetch loyalty points from specific organization
        /// </summary>
        /// <returns>List of loyalty points</returns>
        [HttpPost("search")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FetchLoyaltyPoints([FromBody] FetchLoyaltyPointsRequest filters)
        {
            return await SafeExecuteAsync(async () => await _loyaltyService.FetchLoyaltyPoints(filters),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Create points to organization
        /// </summary>
        /// <returns>Loyalty point balance from organization</returns>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> AddLoyaltyPoints([FromBody] AddLoyaltyPointsEntryRequest command)
        {
            return await SafeExecuteAsync(async () => await _commercialClient.AddLoyaltyPoints(command),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Revert points from specific loyalty
        /// </summary>
        /// <param name="id">Loyalty id</param>
        /// <returns>Loyalty point balance from organization</returns>
        [HttpPost("{id}/revert")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> RevertLoyaltyPoints(Guid id)
        {
            return await SafeExecuteAsync(async () => await _commercialClient.RevertLoyaltyPoints(id),
                HttpMethod.Post);
        }
    }
}