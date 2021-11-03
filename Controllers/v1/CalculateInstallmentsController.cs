using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Infrastructure.Services.v1.CalculateInstallments;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System.Net;
using System.Threading.Tasks;
using static POC.Bff.Web.Domain.Requests.CalculateInstallmentsRequest;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/calculate-installments")]
    [ApiController]
    public class CalculateInstallmentsController : BaseController
    {
        private readonly ICalculateInstallmentsService _calculateInstallmentsService;

        public CalculateInstallmentsController(
            ILogger<CalculateInstallmentsController> logger,
            ICalculateInstallmentsService calculateInstallmentsService,
            IResponseService responseService) : base(logger, responseService)
        {
            _calculateInstallmentsService = calculateInstallmentsService;
        }

        /// <summary>
        ///     Calculate Installments by value.
        /// </summary>
        /// <returns>CalculateValue</returns>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> CalculateInstallmentsByValue(PaymentValueRequest request)
        {
            return await SafeExecuteAsync(async () =>
                await _calculateInstallmentsService.CalculateInstallments(request), HttpMethod.Post);
        }
    }
}