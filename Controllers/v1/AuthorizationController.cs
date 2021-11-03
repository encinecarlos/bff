using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Infrastructure.Services.v1.Authorizations;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/authorization")]
    public class AuthorizationController : BaseController
    {
        private readonly IAuthorizationService _service;

        public AuthorizationController(
            ILogger<AuthorizationController> logger,
            IResponseService responseService,
            IAuthorizationService service) : base(logger, responseService)
        {
            _service = service;
        }

        /// <summary>
        ///     Get authorization detail
        /// </summary>
        /// <returns>Goals,Organizations and User details</returns>
        [HttpGet]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> GetAuthorizationDetail()
        {
            return await SafeExecuteAsync(async () => await _service.GetAuthorizationDetail(), HttpMethod.Get);
        }
    }
}