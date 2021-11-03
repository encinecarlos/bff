//using System.Net;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
//using Microsoft.Extensions.Logging;
//using Sices.Bff.Web.Infrastructure.Clients;
//using Sices.Bff.Web.Swagger;
//using Swashbuckle.AspNetCore.Examples;

//namespace Sices.Bff.Web.Controllers.v1.Configuration
//{
//    [Route("api/v1/phases")]
//    public class PhaseController : BaseController
//    {
//        private readonly IConfigurationClient _configurationClient;

//        public PhaseController(ILogger<PhaseController> logger, IConfigurationClient configurationClient) : base(logger)
//        {
//            _configurationClient = configurationClient;
//        }

//        /// <summary>
//        ///     Find All phases types
//        /// </summary>
//        /// <returns>Phases types</returns>
//        [HttpGet]
//        [SwaggerResponseExample((int) HttpStatusCode.OK, typeof(ResponseSuccessExample))]
//        [SwaggerResponseExample((int) HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
//        public async Task<IActionResult> FindPhase()
//        {
//            return await SafeExecuteAsync(async () => await _configurationClient.FindPhases(), HttpMethod.Get);
//        }
//    }
//}