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
    [Route("api/v1/modules")]
    public class ModuleController : BaseController
    {
        private readonly IComponentClient _componentClient;

        public ModuleController(
            ILogger<ModuleController> logger,
            IResponseService responseService,
            IComponentClient componentClient) : base(logger, responseService)
        {
            _componentClient = componentClient;
        }


        /// <summary>
        ///     Get Module by identifier
        /// </summary>
        /// <param name="erpCode">Module Identifier</param>
        /// <returns> Module </returns>
        [HttpGet("{erpCode}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> GetModule(string erpCode)
        {
            return await SafeExecuteAsync(async () => await _componentClient.GetModule(erpCode), HttpMethod.Get);
        }

        /// <summary>
        ///     Find All Modules by filters
        /// </summary>
        /// <returns>List of Modules</returns>
        [HttpGet]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindModule()
        {
            return await SafeExecuteAsync(async () => await _componentClient.FindModules(), HttpMethod.Get);
        }

        /// <summary>
        ///     Create a new Module
        /// </summary>
        /// <param name="command">Json containing the fields to create the Module</param>
        /// <returns>Module Created</returns>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PostModule(ModuleRequest command)
        {
            return await SafeExecuteAsync(async () => await _componentClient.CreateModule(command), HttpMethod.Post);
        }

        /// <summary>
        ///     Delete Module by identifier
        /// </summary>
        /// <param name="id">Module Identifier</param>
        /// <returns>Status Code 200 if successfully deleted</returns>
        [HttpDelete("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> DeleteModule(Guid id)
        {
            return await SafeExecuteAsync(async () => await _componentClient.DeleteModule(id), HttpMethod.Delete);
        }

        /// <summary>
        ///     Update Module by identifier
        /// </summary>
        /// <param name="id">Module Identifier</param>
        /// <param name="command">Json containing the fields to create the Module</param>
        /// <returns>Module Updated</returns>
        [HttpPut("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PutModule(Guid id, ModuleRequest command)
        {
            return await SafeExecuteAsync(async () => await _componentClient.UpdateModule(id, command), HttpMethod.Put);
        }
    }
}