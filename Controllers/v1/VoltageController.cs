using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Infrastructure.Services.v1.Configurations;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/voltages")]
    public class VoltageController : BaseController
    {
        private readonly IConfigurationService _configurationService;

        public VoltageController(
            ILogger<VoltageController> logger,
            IResponseService responseService,
            IConfigurationService configurationService) : base(logger, responseService)
        {
            _configurationService = configurationService;
        }

        /// <summary>
        ///     Get Voltages
        /// </summary>
        /// <returns>List of voltages</returns>
        [HttpGet]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindVoltage()
        {
            return await SafeExecuteAsync(async () => await _configurationService.FindVoltages(), HttpMethod.Get);
        }

        /// <summary>
        ///     Get Voltage by identifier
        /// </summary>
        /// <param name="id">Voltage identifier</param>
        /// <returns>Voltage</returns>
        [HttpGet("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> GetVoltage(Guid id)
        {
            return await SafeExecuteAsync(async () => await _configurationService.GetVoltage(id), HttpMethod.Get);
        }
    }
}