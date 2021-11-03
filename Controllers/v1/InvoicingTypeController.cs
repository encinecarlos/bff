//using System.Net;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
//using Microsoft.Extensions.Logging;
//using Sices.Bff.Web.Infrastructure.Clients;
//using Sices.Bff.Web.Swagger;
//using Swashbuckle.AspNetCore.Examples;

//namespace Sices.Bff.Web.Controllers.v1.Commercial
//{
//    [Route("api/v1/invoicing-types")]
//    public class InvoicingTypeController : BaseController
//    {
//        private readonly ICommercialClient _commercialClient;

//        public InvoicingTypeController(ILogger<InvoicingTypeController> logger, ICommercialClient commercialClient) :
//            base(logger)
//        {
//            _commercialClient = commercialClient;
//        }

//        /// <summary>
//        ///     Find All invoicing types
//        /// </summary>
//        /// <returns>List of invoicing types</returns>
//        [HttpGet]
//        [SwaggerResponseExample((int) HttpStatusCode.OK, typeof(ResponseSuccessExample))]
//        [SwaggerResponseExample((int) HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
//        public async Task<IActionResult> FindInvoicingType()
//        {
//            return await SafeExecuteAsync(async () => await _commercialClient.FindInvoicingTypes(), HttpMethod.Get);
//        }
//    }
//}