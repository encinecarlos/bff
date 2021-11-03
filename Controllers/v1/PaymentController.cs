using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Services.v1.Organizations;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/payments")]
    public class PaymentController : BaseController
    {
        private readonly IOrganizationService _organizationService;

        public PaymentController(
            ILogger<PaymentController> logger,
            IResponseService responseService,
            IOrganizationService organizationService) : base(logger, responseService)
        {
            _organizationService = organizationService;
        }


        /// <summary>
        ///     Find All Payment Method by language
        /// </summary>
        /// <returns>List of Payment Methods</returns>
        [HttpGet("methods")]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> FindPaymentMethods()
        {
            return await SafeExecuteAsync(async () => await _organizationService.FindPaymentMethods(), HttpMethod.Get);
        }


        /// <summary>
        ///     Find All Payment Conditions by filters
        /// </summary>
        /// <returns>List of payment conditions</returns>
        [HttpGet("conditions")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindPaymentConditions()
        {
            return await SafeExecuteAsync(async () => await _organizationService.FindPaymentConditions(),
                HttpMethod.Get);
        }

        /// <summary>
        ///     Get Payment Condition by identifier
        /// </summary>
        /// <param name="id">Payment Condition Identifier</param>
        /// <returns> Payment Condition </returns>
        [HttpGet("conditions/{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> GetPaymentCondition(Guid id)
        {
            return await SafeExecuteAsync(async () => await _organizationService.GetPaymentCondition(id),
                HttpMethod.Get);
        }

        /// <summary>
        ///     Update Payment Condition by identifier
        /// </summary>
        /// <param name="id">Payment Condition Identifier</param>
        /// <param name="command">Json containing the fields to update payment condition</param>
        /// <returns>Payment Condition updated</returns>
        [HttpPut("conditions/{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> UpdatePaymentCondition(Guid id, PaymentConditionRequest command)
        {
            return await SafeExecuteAsync(async () => await _organizationService.UpdatePaymentCondition(id, command),
                HttpMethod.Put);
        }

        /// <summary>
        ///     Add a new payment condition
        /// </summary>
        /// <param name="request">Json containing the payment condition</param>
        /// <returns>New Payment Condition</returns>
        [HttpPost("conditions")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> CreatePaymentCondition(PaymentConditionRequest request)
        {
            return await SafeExecuteAsync(async () => await _organizationService.CreatePaymentCondition(request),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Delete a payment condition
        /// </summary>
        /// <param name="id">Payment condition id</param>
        [HttpDelete("conditions/{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> DeletePaymentCondition(Guid id)
        {
            return await SafeExecuteAsync(async () => await _organizationService.DeletePaymentCondition(id),
                HttpMethod.Delete);
        }
    }
}