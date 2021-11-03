using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Infrastructure.Services.v1.Organizations;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/discount-types")]
    public class DiscountTypeController : BaseController
    {
        private readonly IOrganizationService _organizationService;

        public DiscountTypeController(
            ILogger<DiscountTypeController> logger,
            IResponseService responseService,
            IOrganizationService organizationService) : base(logger, responseService)
        {
            _organizationService = organizationService;
        }

        /// <summary>
        ///     Find All discount types
        /// </summary>
        /// <returns>List of discount types</returns>
        [HttpGet]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindDiscountType()
        {
            return await SafeExecuteAsync(async () => await _organizationService.FindDiscountTypes(), HttpMethod.Get);
        }
    }
}