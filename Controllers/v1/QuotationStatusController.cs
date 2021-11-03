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
    [Route("api/v1/quotation-status")]
    public class QuotationStatusController : BaseController
    {
        private readonly IOrganizationService _organizationService;

        public QuotationStatusController(
            ILogger<QuotationStatusController> logger,
            IResponseService responseService,
            IOrganizationService organizationService)
            : base(logger, responseService)
        {
            _organizationService = organizationService;
        }


        /// <summary>
        ///     Find All quotation status
        /// </summary>
        /// <returns>List of quotation status types</returns>
        [HttpGet]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindQuotationsStatus()
        {
            return await SafeExecuteAsync(async () => await _organizationService.FindQuotationsStatus(),
                HttpMethod.Get);
        }

        /// <summary>
        ///     Find All quotation status can cancel
        /// </summary>
        /// <returns>List of quotation status types</returns>
        [HttpGet("cancel")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindCancellationQuotationStatus()
        {
            return await SafeExecuteAsync(async () => await _organizationService.FindCancellationQuotationStatus(),
                HttpMethod.Get);
        }

        /// <summary>
        ///     Find All quotation status can delete
        /// </summary>
        /// <returns>List of quotation status types</returns>
        [HttpGet("delete")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindDeletedQuotationStatus()
        {
            return await SafeExecuteAsync(async () => await _organizationService.FindDeletedQuotationStatus(),
                HttpMethod.Get);
        }
    }
}