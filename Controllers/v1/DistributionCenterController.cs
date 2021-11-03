using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Services.v1.DistributionCenters;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/distribution-centers")]
    public class DistributionCenterController : BaseController
    {
        private readonly IDistributionCenterService _distributionCenterService;

        public DistributionCenterController(
            ILogger<DistributionCenterController> logger,
            IResponseService responseService,
            IDistributionCenterService distributionCenterService) : base(logger, responseService)
        {
            _distributionCenterService = distributionCenterService;
        }


        /// <summary>
        ///     Get Distribution Center by identifier
        /// </summary>
        /// <param name="id">Distribution Center identification</param>
        /// <returns>Distribution Center</returns>
        [HttpGet("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> GetDistributionCenter(Guid id)
        {
            return await SafeExecuteAsync(async () => await _distributionCenterService.GetDistributionCenter(id),
                HttpMethod.Get);
        }

        /// <summary>
        ///     Find All Distribution Center by filters
        /// </summary>
        /// <returns>List of Distribution Center</returns>
        [HttpGet]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindDistributionCenter()
        {
            return await SafeExecuteAsync(async () => await _distributionCenterService.FindDistributionCenters(),
                HttpMethod.Get);
        }

        /// <summary>
        ///     Create a new Distribution Center
        /// </summary>
        /// <param name="command">Json containing the fields to create the Distribution Center</param>
        /// <returns>Distribution Center created</returns>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PostDistributionCenter(DistributionCenterRequest command)
        {
            return await SafeExecuteAsync(
                async () => await _distributionCenterService.CreateDistributionCenter(command),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Delete Distribution Center by identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status Code 200 if successfully deleted</returns>
        [HttpDelete("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> DeleteDistributionCenter(Guid id)
        {
            return await SafeExecuteAsync(async () => await _distributionCenterService.DeleteManufacturer(id),
                HttpMethod.Delete);
        }

        /// <summary>
        ///     Update Distribution Center by identifier
        /// </summary>
        /// <param name="id">Distribution Center identifier</param>
        /// <param name="command">Json containing the fields to update the Distribution Center</param>
        /// <returns>Distribution Center Updated</returns>
        [HttpPut("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PutDistributionCenter(Guid id, DistributionCenterRequest command)
        {
            return await SafeExecuteAsync(
                async () => await _distributionCenterService.UpdateDistributionCenter(id, command),
                HttpMethod.Put);
        }

        /// <summary>
        ///     Find All Distribution Centers by filters
        /// </summary>
        /// <returns>List of Distribution Centers</returns>
        [HttpPost("search")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> Search(FindPagedDistributionCentersQueryRequest request)
        {
            return await SafeExecuteAsync(
                async () => await _distributionCenterService.FindPagedDistributionCenters(request), HttpMethod.Post);
        }
    }
}