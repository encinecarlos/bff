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
    [Route("api/v1/tiers")]
    public class LoyaltyTierController : BaseController
    {
        private readonly IConfigurationService _configurationService;

        public LoyaltyTierController(
            ILogger<LoyaltyTierController> logger,
            IResponseService responseService,
            IConfigurationService configurationService) :
            base(logger, responseService)
        {
            _configurationService = configurationService;
        }

        /// <summary>
        ///     Find all Tiers
        /// </summary>
        /// <returns>List of LoyaltyTier</returns>
        [HttpGet]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindLoyaltyTier()
        {
            return await SafeExecuteAsync(async () => await _configurationService.FindTiers(), HttpMethod.Get);
        }
    }
}