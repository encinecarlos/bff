using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Services.v1.Organizations;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/customers")]
    public class CustomerController : BaseController
    {
        private readonly IOrganizationService _organizationService;

        public CustomerController(
            ILogger<CustomerController> logger,
            IResponseService responseService,
            IOrganizationService organizationService) : base(logger, responseService)
        {
            _organizationService = organizationService;
        }

        /// <summary>
        ///     Find Customers from current Organization user
        /// </summary>
        /// <returns>Customers</returns>
        [HttpGet]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> FindCustomer()
        {
            return await SafeExecuteAsync(async () => await _organizationService.FindCustomers(), HttpMethod.Get);
        }

        /// <summary>
        ///     Get Customer from document and current Organization user
        /// </summary>
        /// <returns>Customer</returns>
        [HttpGet("{document}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> GetCustomer(string document)
        {
            return await SafeExecuteAsync(async () => await _organizationService.GetCustomer(document), HttpMethod.Get);
        }

        /// <summary>
        ///     Create Customer from current organization user
        /// </summary>
        /// <returns>Customer</returns>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PostCustomer(CustomerRequest command)
        {
            return await SafeExecuteAsync(async () => await _organizationService.CreateCustomer(command),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Change Customer from current organization user
        /// </summary>
        /// <returns>Customer</returns>
        [HttpPut("{document}")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PutCustomer(string document, CustomerRequest command)
        {
            return await SafeExecuteAsync(async () => await _organizationService.UpdateCustomer(document, command),
                HttpMethod.Put);
        }

        /// <summary>
        ///     Delete Customer from current organization user
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{document}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> DeleteCustomer(string document)
        {
            return await SafeExecuteAsync(async () => await _organizationService.DeleteCustomer(document),
                HttpMethod.Delete);
        }
    }
}