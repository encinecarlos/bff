using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Infrastructure.Services.v1.ZipCodes;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/address")]
    public class ZipCodeController : BaseController
    {
        private readonly IZipCodeService _zipCodeService;

        public ZipCodeController(
            ILogger<ZipCodeController> logger,
            IResponseService responseService,
            IZipCodeService zipCodeService) : base(logger, responseService)
        {
            _zipCodeService = zipCodeService;
        }

        /// <summary>
        ///     Get ZipCode by country and zipcode
        /// </summary>
        /// <param name="countryCode">Country code identification</param>
        /// <param name="zipcode">ZipCode identification</param>
        /// <returns>ZipCode</returns>
        [HttpGet("{countryCode}/{zipcode}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> GetZipCode(string countryCode, string zipcode)
        {
            return await SafeExecuteAsync(async () => await _zipCodeService.GetAddress(zipcode, countryCode),
                HttpMethod.Get);
        }

        /// <summary>
        ///     Get ZipCode by zipcode
        /// </summary>
        /// <param name="zipcode">ZipCode identification</param>
        /// <returns>ZipCode</returns>
        [HttpGet("{zipcode}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> GetZipCode(string zipcode)
        {
            return await SafeExecuteAsync(async () => await _zipCodeService.GetAddress(zipcode), HttpMethod.Get);
        }
    }
}