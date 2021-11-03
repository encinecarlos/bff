using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/varieties")]
    public class VarietyController : BaseController
    {
        private readonly IComponentClient _componentClient;

        public VarietyController(
            ILogger<VarietyController> logger,
            IResponseService responseService,
            IComponentClient componentClient) : base(logger, responseService)
        {
            _componentClient = componentClient;
        }


        /// <summary>
        ///     Get Variety by identifier
        /// </summary>
        /// <param name="erpCode">Variety Identifier</param>
        /// <returns> Variety </returns>
        [HttpGet("{erpCode}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> GetVariety(string erpCode)
        {
            return await SafeExecuteAsync(async () => await _componentClient.GetVariety(erpCode), HttpMethod.Get);
        }

        /// <summary>
        ///     Find All Varieties by filters
        /// </summary>
        /// <returns>List of Varieties</returns>
        [HttpGet]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindVariety()
        {
            return await SafeExecuteAsync(async () => await _componentClient.FindVarieties(), HttpMethod.Get);
        }

        /// <summary>
        ///     Create a new Variety
        /// </summary>
        /// <param name="command">Json containing the fields to create the Variety</param>
        /// <returns>Variety Created</returns>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PostVariety(VarietyRequest command)
        {
            return await SafeExecuteAsync(async () => await _componentClient.CreateVariety(command), HttpMethod.Post);
        }

        /// <summary>
        ///     Delete Variety by identifier
        /// </summary>
        /// <param name="id">Variety Identifier</param>
        /// <returns>Status Code 200 if successfully deleted</returns>
        [HttpDelete("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> DeleteVariety(Guid id)
        {
            return await SafeExecuteAsync(async () => await _componentClient.DeleteVariety(id), HttpMethod.Delete);
        }

        /// <summary>
        ///     Update Variety by identifier
        /// </summary>
        /// <param name="id">Variety Identifier</param>
        /// <param name="command">Json containing the fields to create the Variety</param>
        /// <returns>Variety Updated</returns>
        [HttpPut("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PutVariety(Guid id, VarietyRequest command)
        {
            return await SafeExecuteAsync(async () => await _componentClient.UpdateVariety(id, command),
                HttpMethod.Put);
        }
    }
}