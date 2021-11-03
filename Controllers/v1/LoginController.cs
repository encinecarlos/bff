using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Services.v1.Accounts;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/login")]
    public class LoginController : BaseController
    {
        private readonly IAccountService _accountService;

        public LoginController(
            ILogger<LoginController> logger,
            IResponseService responseService,
            IAccountService accountService) : base(logger, responseService)
        {
            _accountService = accountService;
        }

        /// <summary>
        ///     Register login in system
        /// </summary>
        /// <param name="request">Json containing fields necessary to effect login</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            return await SafeExecuteAsync(async () => await _accountService.RegisterNewLogin(request), HttpMethod.Post);
        }
    }
}