using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Dtos;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Services.v1.Memorials;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/memorial")]
    public class MemorialController : BaseController
    {
        private readonly IMemorialService _memorialService;

        public MemorialController(
            ILogger<MemorialController> logger,
            IResponseService responseService,
            IMemorialService memorialService) : base(logger, responseService)
        {
            _memorialService = memorialService;
        }

        /// <summary>
        ///     Create a new Memorial
        /// </summary>
        /// <param name="command">Json containing the name and estimated for expiration date of memorial</param>
        /// <returns>Id Memorial Created</returns>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PostMemorial(CreateMemorialRequest command)
        {
            return await SafeExecuteAsync(async () => await _memorialService.CreateMemorial(command), HttpMethod.Post);
        }

        /// <summary>
        ///     Find memorial by filters
        /// </summary>
        /// <returns>List of memorial</returns>
        [HttpPost("search")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindMemorial(FindMemorialsRequest query)
        {
            return await SafeExecuteAsync(async () => await _memorialService.FindMemorials(query), HttpMethod.Post);
        }

        /// <summary>
        ///     Get memorial by identifier
        /// </summary>
        /// <returns>List of memorial</returns>
        [HttpGet("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> GetMemorial(Guid id)
        {
            return await SafeExecuteAsync(async () => await _memorialService.GetMemorial(id), HttpMethod.Get);
        }

        /// <summary>
        ///     Find  available components in memorial
        /// </summary>
        /// <param name="id">Memorial identifier</param>
        /// <param name="query">Query String</param>
        /// <returns></returns>
        [HttpPost("{id}/components/search")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindComponentsInMemorial(Guid id, [FromBody] FindComponentDto query)
        {
            return await SafeExecuteAsync(async () => await _memorialService.FindMemorialComponents(id, query),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Save changes made to components
        /// </summary>
        /// <param name="id">Memorial identifier</param>
        /// <param name="command">Object request contains token session</param>
        /// <returns></returns>
        [HttpPut("{id}/components")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> SaveComponents(Guid id, [FromBody] SaveMemorialRequest command)
        {
            return await SafeExecuteAsync(async () => await _memorialService.SaveMemorialComponents(id, command),
                HttpMethod.Put);
        }


        /// <summary>
        ///     Change cmv component from memorial
        /// </summary>
        /// <param name="id">Memorial identifier</param>
        /// <param name="erpCode">Component ErpCode</param>
        /// <param name="command">Object request contains fields to use for change cmv</param>
        /// <returns></returns>
        [HttpPatch("{id}/components/{erpCode}/cmv")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> UpdateCmv(Guid id, string erpCode,
            [FromBody] UpdateMemorialComponentCmvDto command)
        {
            return await SafeExecuteAsync(
                async () => await _memorialService.UpdateMemorialComponentCmv(id, erpCode, command), HttpMethod.Patch);
        }


        /// <summary>
        ///     Batch change of component cmv
        /// </summary>
        /// <param name="id">Memorial identifier</param>
        /// <param name="command">Object request contains fields to use for change cmv</param>
        /// <returns></returns>
        [HttpPatch("{id}/components/cmv")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> BatchChangeComponentCmv(Guid id,
            [FromBody] BatchChangeMemorialComponentDto command)
        {
            return await SafeExecuteAsync(
                async () => await _memorialService.BatchChangeMemorialComponentCmv(id, command), HttpMethod.Patch);
        }


        /// <summary>
        ///     Change markup memorial component
        /// </summary>
        /// <param name="id">Memorial identifier</param>
        /// <param name="erpCode">Component ErpCode</param>
        /// <param name="command">Object request contains fields to use for change markup</param>
        /// <returns></returns>
        [HttpPatch("{id}/components/{erpCode}/markup")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> UpdateMarkup(Guid id, string erpCode,
            [FromBody] UpdateMemorialComponentMarkupDto command)
        {
            return await SafeExecuteAsync(
                async () => await _memorialService.UpdateMemorialComponentMarkup(id, erpCode, command),
                HttpMethod.Patch);
        }


        /// <summary>
        ///     Batch change of component markup
        /// </summary>
        /// <param name="id">Memorial identifier</param>
        /// <param name="command">Object request contains fields to use for change cmv</param>
        /// <returns></returns>
        [HttpPatch("{id}/components/markup")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> BatchChangeComponentMarkup(Guid id,
            [FromBody] BatchChangeComponentMarkupDto command)
        {
            return await SafeExecuteAsync(async () => await _memorialService.BatchChangeComponentMarkup(id, command),
                HttpMethod.Patch);
        }

        /// <summary>
        ///     Clone memorial
        /// </summary>
        /// <param name="id">Request to clone</param>
        /// <returns></returns>
        [HttpPost("{id}/clone")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> CloneMemorial(Guid id)
        {
            return await SafeExecuteAsync(async () => await _memorialService.CloneMemorial(id), HttpMethod.Post);
        }

        /// <summary>
        ///     Publish memorial
        /// </summary>
        /// <param name="id">Request to clone</param>
        /// <returns></returns>
        [HttpPost("{id}/publish")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> PublishMemorial(Guid id)
        {
            return await SafeExecuteAsync(async () => await _memorialService.PublishMemorial(id), HttpMethod.Post);
        }

        /// <summary>
        ///     Batch Delete memorials from identifiers in command
        /// </summary>
        /// <param name="command">Json containing the identifiers of memorials</param>
        /// <returns></returns>
        [HttpPost("delete")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> BatchDeleteMemorial(BatchDeleteMemorialsRequest command)
        {
            return await SafeExecuteAsync(async () => await _memorialService.BatchDeleteMemorials(command),
                HttpMethod.Post);
        }

        /// <summary>
        ///     get memorial session
        /// </summary>
        /// <returns>List of memorial</returns>
        [HttpGet("session")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> GetSession()
        {
            return await SafeExecuteAsync(async () => await _memorialService.GetMemorialSession(), HttpMethod.Get);
        }


        [HttpGet("page/detail/configuration")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> GetMemorialDetailConfig()
        {
            return await SafeExecuteAsync(async () => await _memorialService.GetMemorialDetailConfig(), HttpMethod.Get);
        }

        /// <summary>
        ///     Find grouped available components in memorial
        /// </summary>
        /// <param name="id">Memorial identifier</param>
        /// <param name="query">Query String</param>
        /// <returns></returns>
        [HttpPost("{id}/group-components/search")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> FindGroupedComponents(Guid id, FindMemorialDto query)
        {
            return await SafeExecuteAsync(async () => await _memorialService.FindMemorialGroupComponents(id, query),
                HttpMethod.Post);
        }

        /// <summary>
        ///     Change Memorial
        /// </summary>
        /// <param name="id">Memorial identifier</param>
        /// <param name="query">Query String</param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> ChangeMemorial(Guid id, ChangeMemorialRequest query)
        {
            return await SafeExecuteAsync(async () => await _memorialService.ChangeMemorial(id, query),
                HttpMethod.Patch);
        }
    }
}