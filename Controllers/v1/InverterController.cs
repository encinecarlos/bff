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
    [Route("api/v1/inverters")]
    public class InverterController : BaseController
    {
        private readonly IComponentClient _componentClient;

        public InverterController(
            ILogger<InverterController> logger,
            IResponseService responseService,
            IComponentClient componentClient) : base(logger, responseService)
        {
            _componentClient = componentClient;
        }


        /// <summary>
        ///     Get Inverter by identifier
        /// </summary>
        /// <param name="erpCode">Inverter Identifier</param>
        /// <returns> Inverter </returns>
        [HttpGet("{erpCode}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> GetInverter(string erpCode)
        {
            return await SafeExecuteAsync(async () => await _componentClient.GetInverter(erpCode), HttpMethod.Get);
        }

        /// <summary>
        ///     Find All Inverters by filters
        /// </summary>
        /// <returns>List of Inverters</returns>
        [HttpGet]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindInverter()
        {
            return await SafeExecuteAsync(async () => await _componentClient.FindInverters(), HttpMethod.Get);
        }

        /// <summary>
        ///     Create a new Inverter
        /// </summary>
        /// <param name="command">Json containing the fields to create the Inverter</param>
        /// <returns>Inverter Created</returns>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PostInverter(InverterRequest command)
        {
            return await SafeExecuteAsync(async () => await _componentClient.CreateInverter(command), HttpMethod.Post);
        }

        /// <summary>
        ///     Delete Inverter by identifier
        /// </summary>
        /// <param name="id">Inverter Identifier</param>
        /// <returns>Status Code 200 if successfully deleted</returns>
        [HttpDelete("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> DeleteInverter(Guid id)
        {
            return await SafeExecuteAsync(async () => await _componentClient.DeleteInverter(id), HttpMethod.Delete);
        }

        /// <summary>
        ///     Update Inverter by identifier
        /// </summary>
        /// <param name="id">Inverter Identifier</param>
        /// <param name="command">Json containing the fields to create the Inverter</param>
        /// <returns>Inverter Updated</returns>
        [HttpPut("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PutInverter(Guid id, InverterRequest command)
        {
            return await SafeExecuteAsync(async () => await _componentClient.UpdateInverter(id, command),
                HttpMethod.Put);
        }
    }
}