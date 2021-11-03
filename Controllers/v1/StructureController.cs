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
    [Route("api/v1/structures")]
    public class StructureController : BaseController
    {
        private readonly IComponentClient _componentClient;

        public StructureController(
            ILogger<StructureController> logger,
            IResponseService responseService,
            IComponentClient componentClient) : base(logger, responseService)
        {
            _componentClient = componentClient;
        }

        /// <summary>
        ///     Get Structure by identifier
        /// </summary>
        /// <param name="erpCode">Structure Identifier</param>
        /// <returns> Structure </returns>
        [HttpGet("{erpCode}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> GetStructure(string erpCode)
        {
            return await SafeExecuteAsync(async () => await _componentClient.GetStructure(erpCode), HttpMethod.Get);
        }

        /// <summary>
        ///     Find All Structures by filters
        /// </summary>
        /// <returns>List of Structures</returns>
        [HttpGet]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindStructure()
        {
            return await SafeExecuteAsync(async () => await _componentClient.FindStructures(), HttpMethod.Get);
        }

        /// <summary>
        ///     Create a new Structure
        /// </summary>
        /// <param name="command">Json containing the fields to create the Structure</param>
        /// <returns>Structure Created</returns>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PostStructure(StructureRequest command)
        {
            return await SafeExecuteAsync(async () => await _componentClient.CreateStructure(command), HttpMethod.Post);
        }

        /// <summary>
        ///     Delete Structure by identifier
        /// </summary>
        /// <param name="id">Structure Identifier</param>
        /// <returns>Status Code 200 if successfully deleted</returns>
        [HttpDelete("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> DeleteStructure(Guid id)
        {
            return await SafeExecuteAsync(async () => await _componentClient.DeleteStructure(id), HttpMethod.Delete);
        }

        /// <summary>
        ///     Update Structure by identifier
        /// </summary>
        /// <param name="id">Structure Identifier</param>
        /// <param name="command">Json containing the fields to create the Structure</param>
        /// <returns>Structure Updated</returns>
        [HttpPut("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PutStructure(Guid id, StructureRequest command)
        {
            return await SafeExecuteAsync(async () => await _componentClient.UpdateStructure(id, command),
                HttpMethod.Put);
        }
    }
}