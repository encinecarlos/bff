using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Services.v1.Components;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/system-power")]
    public class SystemPowerController : BaseController
    {
        private readonly IComponentService _componentService;

        public SystemPowerController(
            ILogger<SystemPowerController> logger,
            IResponseService responseService,
            IComponentService componentService) : base(logger, responseService)
        {
            _componentService = componentService;
        }

        /// <summary>
        ///     Validate System power of components
        /// </summary>
        /// <returns> Components </returns>
        [HttpPost("validate")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> ValidateSystemPower(ValidateComponentRequest command)
        {
            return await SafeExecuteAsync(async () => await _componentService.ValidateSystemPower(command),
                HttpMethod.Post);
        }
    }
}