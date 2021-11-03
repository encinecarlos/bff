using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Services.v1.Systems;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/systems")]
    public class SystemController : BaseController
    {
        private readonly ISystemServiceHandler _systemService;

        public SystemController(
            ILogger<SystemController> logger,
            IResponseService responseService,
            ISystemServiceHandler systemService) : base(logger, responseService)
        {
            _systemService = systemService;
        }

        /// <summary>
        ///     Setup Configuration for PvSystem
        /// </summary>
        /// <returns></returns>
        [HttpGet("setup/configuration")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindPvSystemSetupConfig(Guid quotationId, bool automaticSystem,
            Guid promotionId)
        {
            return await SafeExecuteAsync(async () => await _systemService
                .FindSystemSetupConfig(quotationId, automaticSystem, promotionId), HttpMethod.Get);
        }

        /// <summary>
        ///     Create a new System
        /// </summary>
        /// <param name="command">Json containing the fields to create the System</param>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PostSystem(GenerateNewSystemRequest command)
        {
            return await SafeExecuteAsync(async () => await _systemService
                .GenerateNewSystem(command), HttpMethod.Post);
        }

        /// <summary>
        ///     Validate PvSystem
        /// </summary>
        /// <returns></returns>
        [HttpPost("validate/system")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PvSystemInfoValidate(ValidateComponentRequest validateComponent)
        {
            return await SafeExecuteAsync(async () => await _systemService
                .SystemInfoValidate(validateComponent), HttpMethod.Post);
        }
    }
}