using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Infrastructure.Services.v1.Medias;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/medias")]
    public class MediaController : BaseController
    {
        private readonly IMediaService _mediaService;

        public MediaController(
            ILogger<MediaController> logger,
            IMediaService mediaService,
            IResponseService responseService) : base(logger, responseService)
        {
            _mediaService = mediaService;
        }

        /// <summary>
        ///     Upload a new media on cloud storage
        /// </summary>
        /// <param name="command">Json containing the media request base64 string</param>
        /// <returns>Media successfully uploaded to cloud storage</returns>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> UploadMedia(object command)
        {
            return await SafeExecuteAsync(async () => await _mediaService.UploadMedia(command), HttpMethod.Post);
        }


        /// <summary>
        ///     Check media to selected
        /// </summary>
        /// <returns>Media successfully updated</returns>
        [HttpPatch("{keyName}/check")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> CheckMedia(string keyName)
        {
            return await SafeExecuteAsync(async () => await _mediaService.CheckMedia(keyName), HttpMethod.Patch);
        }


        /// <summary>
        ///     Find medias
        /// </summary>
        /// <returns>Media successfully updated</returns>
        [HttpGet]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> FindMedias()
        {
            return await SafeExecuteAsync(async () => await _mediaService.FindMedias(), HttpMethod.Get);
        }
    }
}