using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Bff.Web.Infrastructure.Services.v1.Components;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/components")]
    public class ComponentController : BaseController
    {
        private readonly IComponentClient _componentClient;
        private readonly IComponentService _componentService;

        public ComponentController(
            ILogger<ComponentController> logger,
            IComponentClient componentClient,
            IResponseService responseService,
            IComponentService componentService) : base(logger, responseService)

        {
            _componentService = componentService;
            _componentClient = componentClient;
        }

        /// <summary>
        ///     Get grouped components by tier.
        /// </summary>
        /// <param name="tierId">Tier Id</param>
        /// <returns>Grouped Components by Type</returns>
        [HttpGet("group")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> GetGroupedComponentsByTier(int tierId)
        {
            return await SafeExecuteAsync(async () => await _componentService.GetGroupedComponentsByTier(tierId),
                HttpMethod.Get);
        }

        /// <summary>
        ///     Find pv system components by filters
        /// </summary>
        /// <param name="request">Query string contains fields</param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindPublishedMemorialComponentsGroupedByType(
            [FromQuery] FindPublishedMemorialComponentsGroupedByTypeRequest request)
        {
            return await SafeExecuteAsync(
                async () => await _componentService.FindPublishedMemorialComponentsGroupedByType(request),
                HttpMethod.Get);
        }

        /// <summary>
        ///     Get components by pricing tier.
        /// </summary>
        /// <returns>Grouped Components by Type</returns>
        [HttpGet("tier/{tierId}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> GetComponentsByTier(int tierId)
        {
            return await SafeExecuteAsync(async () => await _componentService.GetComponentsByTier(tierId),
                HttpMethod.Get);
        }

        /// <summary>
        ///     Find All components by type/search term
        /// </summary>
        /// <returns>List of components</returns>
        [HttpPost("search")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindComponentsBySearchTerm([FromBody] ComponentListBySearchTermRequest request)
        {
            return await SafeExecuteAsync(async () => await _componentService.FindComponentsBySearchTermAsync(request),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Update component by erp code
        /// </summary>
        /// <param name="erpCode">Erp code to update component</param>
        /// <param name="request">Query string contains data to update component</param>
        /// <returns></returns>
        [HttpPatch("{erpCode}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> UpdateComponentByErpCode(string erpCode,
            ComponentUpdateByErpCodeRequest request)
        {
            return await SafeExecuteAsync(async () => await _componentClient.UpdateComponentByErpCode(erpCode, request),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Find component by erp code
        /// </summary>
        /// <returns>Component detail</returns>
        [HttpGet("{erpCode}")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> FindComponentDetailByErpCode(string erpCode)
        {
            return await SafeExecuteAsync(async () => await _componentClient.FindComponentDetailByErpCode(erpCode),
                HttpMethod.Get);
        }

        /// <summary>
        ///     Find tiers and distribution centers for components
        /// </summary>
        /// <returns>Tiers and distribution centers</returns>
        [HttpGet("tiers-dc")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindTiersAndDistributionCenters()
        {
            return await SafeExecuteAsync(async () => await _componentService.FindTiersAndDistributionCenters(),
                HttpMethod.Get);
        }
    }
}