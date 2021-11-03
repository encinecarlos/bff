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
    [Route("api/v1/string-box")]
    public class StringBoxController : BaseController
    {
        private readonly IComponentClient _componentClient;

        public StringBoxController(
            ILogger<StringBoxController> logger,
            IResponseService responseService,
            IComponentClient componentClient) : base(logger, responseService)
        {
            _componentClient = componentClient;
        }

        /// <summary>
        ///     Get StringBox by identifier
        /// </summary>
        /// <param name="erpCode">StringBox Identifier</param>
        /// <returns> StringBox </returns>
        [HttpGet("{erpCode}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> GetStringBox(string erpCode)
        {
            return await SafeExecuteAsync(async () => await _componentClient.GetStringBox(erpCode), HttpMethod.Get);
        }

        /// <summary>
        ///     Find All StringBoxes by filters
        /// </summary>
        /// <returns>List of StringBoxes</returns>
        [HttpGet]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindStringBox()
        {
            return await SafeExecuteAsync(async () => await _componentClient.FindStringBoxes(), HttpMethod.Get);
        }

        /// <summary>
        ///     Create a new StringBox
        /// </summary>
        /// <param name="command">Json containing the fields to create the StringBox</param>
        /// <returns>StringBox Created</returns>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PostStringBox(StringBoxRequest command)
        {
            return await SafeExecuteAsync(async () => await _componentClient.CreateStringBox(command), HttpMethod.Post);
        }

        /// <summary>
        ///     Delete StringBox by identifier
        /// </summary>
        /// <param name="id">StringBox Identifier</param>
        /// <returns>Status Code 200 if successfully deleted</returns>
        [HttpDelete("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> DeleteStringBox(Guid id)
        {
            return await SafeExecuteAsync(async () => await _componentClient.DeleteStringBox(id), HttpMethod.Delete);
        }

        /// <summary>
        ///     Update StringBox by identifier
        /// </summary>
        /// <param name="id">StringBox Identifier</param>
        /// <param name="command">Json containing the fields to create the StringBox</param>
        /// <returns>StringBox Updated</returns>
        [HttpPut("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PutStringBox(Guid id, StringBoxRequest command)
        {
            return await SafeExecuteAsync(async () => await _componentClient.UpdateStringBox(id, command),
                HttpMethod.Put);
        }
    }
}