using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Services.v1.Reports;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/reports")]
    public class ReportController : BaseController
    {
        private readonly IReportService _reportService;

        public ReportController(
            ILogger<ReportController> logger,
            IResponseService responseService,
            IReportService reportService) : base(logger, responseService)
        {
            _reportService = reportService;
        }

        /// <summary>
        ///     Create financial report csv
        /// </summary>
        /// <returns></returns>
        [HttpPost("financial/csv")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> CreateFinancialReport(CreateFinancialReportRequest command)
        {
            return await SafeExecuteAsync(async () => await _reportService.CreateFinancialReport(command),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Create after sales report csv
        /// </summary>
        /// <returns></returns>
        [HttpPost("after-sales/csv")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> CreateAfterSalesReport(CreateAfterSalesReportRequest command)
        {
            return await SafeExecuteAsync(async () => await _reportService.CreateAfterSalesReport(command),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Create production report csv
        /// </summary>
        /// <returns></returns>
        [HttpPost("production/csv")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> CreateProductionReport(CreateProductionReportRequest command)
        {
            return await SafeExecuteAsync(async () => await _reportService.CreateProductionReport(command),
                HttpMethod.Post);
        }
    }
}