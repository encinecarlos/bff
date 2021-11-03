using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Infrastructure.Services.v1.Configurations;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/states-codes")]
    public class StateCodeController : BaseController
    {
        private readonly IConfigurationService _configurationService;

        public StateCodeController(
            ILogger<StateCodeController> logger,
            IResponseService responseService,
            IConfigurationService configurationService) :
            base(logger, responseService)
        {
            _configurationService = configurationService;
        }

        /// <summary>
        ///     Find all states code
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns>Codes</returns>
        [HttpGet]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindStatesCode(string countryCode)
        {
            return await SafeExecuteAsync(async () => await _configurationService.FindStatesCode(countryCode),
                HttpMethod.Get);
        }


        /// <summary>
        ///     Find all states code and tiers
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns>Codes</returns>
        [HttpGet("tiers")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindStatesCodeAndTiers(string countryCode)
        {
            return await SafeExecuteAsync(async () => await _configurationService.FindStatesCodeTier(countryCode),
                HttpMethod.Get);
        }

        /// <summary>
        ///     Find all states code, distribution centers and internal users
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns>Codes</returns>
        [HttpGet("distribution-centers/internal-users")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindStatesCodeDistributionCentersInternalUsers(string countryCode)
        {
            return await SafeExecuteAsync(
                async () => await _configurationService.FindStatesCodeDistributionCentersInternalUsers(countryCode),
                HttpMethod.Get);
        }

        /// <summary>
        ///     Find all states code, distribution centers and internal users
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns>Codes</returns>
        [HttpGet("tiers/internal-users")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindStatesCodeTiersInternalUsers(string countryCode)
        {
            return await SafeExecuteAsync(
                async () => await _configurationService.FindStatesCodeTiersInternalUsers(countryCode), HttpMethod.Get);
        }
    }
}