using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Infrastructure.Services.v1.Components;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/datasheet")]
    public class DataSheetController : BaseController
    {
        private readonly IComponentService _componentService;

        public DataSheetController(
            ILogger<DataSheetController> logger,
            IResponseService responseService,
            IComponentService componentService) : base(logger, responseService)
        {
            _componentService = componentService;
        }

        /// <summary>
        ///     Get Data sheet component by identifier
        /// </summary>
        /// <param name="erpCode">Component ErpCode</param>
        /// <returns> Data sheet component</returns>
        [HttpGet("{erpCode}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> GetDataSheet(string erpCode)
        {
            return await SafeExecuteAsync(async () => await _componentService.GetDataSheet(erpCode), HttpMethod.Get);
        }
    }
}