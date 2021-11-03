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
    [Route("api/v1/mlpes")]
    public class MlpeController : BaseController
    {
        private readonly IComponentClient _componentClient;

        public MlpeController(
            ILogger<MlpeController> logger,
            IResponseService responseService,
            IComponentClient componentClient) : base(logger, responseService)
        {
            _componentClient = componentClient;
        }


        /// <summary>
        ///     Get Mlpe by identifier
        /// </summary>
        /// <param name="erpCode">Mlpe Identifier</param>
        /// <returns> Mlpe </returns>
        [HttpGet("{erpCode}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> GetMlpe(string erpCode)
        {
            return await SafeExecuteAsync(async () => await _componentClient.GetMlpe(erpCode), HttpMethod.Get);
        }

        /// <summary>
        ///     Find All MLPEs by filters
        /// </summary>
        /// <returns>List of MLPEs</returns>
        [HttpGet]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> GetMlpe()
        {
            return await SafeExecuteAsync(async () => await _componentClient.FindMlpes(), HttpMethod.Get);
        }

        /// <summary>
        ///     Create a new Mlpe
        /// </summary>
        /// <param name="command">Json containing the fields to create the Mlpe</param>
        /// <returns>Mlpe Created</returns>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PostMlpe(MlpeRequest command)
        {
            return await SafeExecuteAsync(async () => await _componentClient.CreateMlpe(command), HttpMethod.Post);
        }

        /// <summary>
        ///     Delete Mlpe by identifier
        /// </summary>
        /// <param name="id">Mlpe Identifier</param>
        /// <returns>Status Code 200 if successfully deleted</returns>
        [HttpDelete("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> DeleteMlpe(Guid id)
        {
            return await SafeExecuteAsync(async () => await _componentClient.DeleteMlpe(id), HttpMethod.Delete);
        }

        /// <summary>
        ///     Update Mlpe by identifier
        /// </summary>
        /// <param name="id">Mlpe Identifier</param>
        /// <param name="command">Json containing the fields to create the Mlpe</param>
        /// <returns>Mlpe Updated</returns>
        [HttpPut("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PutMlpe(Guid id, MlpeRequest command)
        {
            return await SafeExecuteAsync(async () => await _componentClient.UpdateMlpe(id, command), HttpMethod.Put);
        }
    }
}