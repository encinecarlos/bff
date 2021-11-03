using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Services.v1.Configurations;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/structure-variety")]
    public class StructureVarietyController : BaseController
    {
        private readonly IConfigurationService _configurationService;

        public StructureVarietyController(
            ILogger<StructureVarietyController> logger,
            IResponseService responseService,
            IConfigurationService configurationService) : base(logger, responseService)
        {
            _configurationService = configurationService;
        }

        /// <summary>
        ///     Get structure varieties
        /// </summary>
        /// <returns>List of structure varieties </returns>
        [HttpGet]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindStructureVariety()
        {
            return await SafeExecuteAsync(async () => await _configurationService.FindStructureVarieties(),
                HttpMethod.Get);
        }

        /// <summary>
        ///     Get structure variety by identifier
        /// </summary>
        /// <param name="id"> structure variety identifier</param>
        /// <returns> structure variety</returns>
        [HttpGet("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> GetStructureVariety(Guid id)
        {
            return await SafeExecuteAsync(async () => await _configurationService.GetStructureVariety(id),
                HttpMethod.Get);
        }

        /// <summary>
        ///     Add a new  structure variety
        /// </summary>
        /// <param name="request">Json containing the  structure variety</param>
        /// <returns> structure variety</returns>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PostStructureVariety(StructureVarietyRequest request)
        {
            return await SafeExecuteAsync(async () => await _configurationService.CreateStructureVariety(request),
                HttpMethod.Get);
        }
    }
}