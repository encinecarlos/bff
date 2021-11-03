using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Refit;
using POC.Bff.Web.Infrastructure;
using POC.Shared.Responses.Contracts;
using System;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class BaseController : Controller
    {
        private const string CorrelationHeader = "X-Correlation-ID";
        private const string LogInformationMessage = "Execute Bff at correlation: {0}";
        private const string LogErrorMessage = "Api exception: {0} at correlation: {1}";

        private const string LogInternalServerErrorMessage =
            "System is unavailable, contact your administrator and enter the error code: {0}";

        private readonly ILogger _logger;
        private readonly IResponseService _responseService;

        public BaseController(ILogger logger, IResponseService responseService)
        {
            _logger = logger;
            _responseService = responseService;
        }

        protected async Task<IActionResult> SafeExecuteAsync(Func<Task<BffResponse>> action, HttpMethod verb)
        {
            Request.Headers.TryGetValue(CorrelationHeader, out var xCorrelationHeaderValue);

            var correlation = xCorrelationHeaderValue.Count > 0 ? xCorrelationHeaderValue.ToString() : string.Empty;

            try
            {
                _logger.LogInformation(string.Format(LogInformationMessage, correlation));

                var response = await action();

                return ReturnByRestVerb(response, verb);
            }
            catch (ApiException e)
            {
                _logger.LogError(e, string.Format(LogErrorMessage, e.Message, correlation), correlation);

                var response = CreateFailResponseApiException(e);

                return StatusCode((int)e.StatusCode, response);
            }
            catch (Exception e)
            {
                var response = _responseService.CreateFailResponse()
                    .AddNotification(string.Format(LogInternalServerErrorMessage, correlation));

                _logger.LogCritical(e, e.Message, correlation);

                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            }
        }

        private BffResponse CreateFailResponseApiException(ApiException e)
        {
            return !string.IsNullOrEmpty(e.Content)
                ? JsonConvert.DeserializeObject<BffResponse>(e.Content)
                : _responseService.CreateFailResponse().AddNotification(e.Message);
            ;
        }

        private IActionResult ReturnByRestVerb<TResponse>(TResponse result, HttpMethod verb)
            where TResponse : BffResponse
        {
            switch (verb)
            {
                case HttpMethod.Get when result.Data is null:
                    return NotFound(result);
                case HttpMethod.Delete when !result.Success:
                    return BadRequest(result);
                default:
                    return Ok(result);
            }
        }
    }
}