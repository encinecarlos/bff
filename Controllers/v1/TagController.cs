using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Logging;
using POC.Bff.Web.Domain.Requests;
using POC.Bff.Web.Infrastructure.Clients;
using POC.Bff.Web.Infrastructure.Services.v1.Tags;
using POC.Bff.Web.Swagger;
using POC.Shared.Responses.Contracts;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Net;
using System.Threading.Tasks;

namespace POC.Bff.Web.Controllers.v1
{
    [Route("api/v1/tags")]
    public class TagController : BaseController
    {
        private readonly IConfigurationClient _configurationClient;
        private readonly ITagService _tagService;

        public TagController(
            ILogger<TagController> logger,
            IConfigurationClient configurationClient,
            ITagService tagService,
            IResponseService responseService) : base(logger, responseService)
        {
            _configurationClient = configurationClient;
            _tagService = tagService;
        }

        /// <summary>
        ///     Find all tags
        /// </summary>
        /// <returns>Tags</returns>
        [HttpGet]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> FindAllTags()
        {
            return await SafeExecuteAsync(async () => await _configurationClient.FindTags(), HttpMethod.Get);
        }

        /// <summary>
        ///     Get Tag by identifier
        /// </summary>
        /// <param name="id">Tag identifier</param>
        /// <returns>Tag</returns>
        [HttpGet("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.OK, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        public async Task<IActionResult> GetTagById(Guid id)
        {
            return await SafeExecuteAsync(async () => await _configurationClient.GetTagById(id), HttpMethod.Get);
        }

        /// <summary>
        ///     Add a new tag
        /// </summary>
        /// <param name="request">Json containing the tag</param>
        /// <returns>New Tag</returns>
        [HttpPost]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> CreateNewtag(CreateTagRequest request)
        {
            return await SafeExecuteAsync(async () => await _configurationClient.CreateTag(request), HttpMethod.Post);
        }

        /// <summary>
        ///     Update a tag
        /// </summary>
        /// <param name="id">Tag id</param>
        /// <param name="request">Json containing the tag</param>
        /// <returns>Updated Tag</returns>
        [HttpPut("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> UpdateTagById(Guid id, UpdateTagRequest request)
        {
            return await SafeExecuteAsync(async () => await _configurationClient.UpdateTag(id, request),
                HttpMethod.Put);
        }

        /// <summary>
        ///     Delete a tag
        /// </summary>
        /// <param name="id">Tag id</param>
        /// <returns>Delete Tag</returns>
        [HttpDelete("{id}")]
        [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(ResponseSuccessExample))]
        [SwaggerResponseExample((int)HttpStatusCode.NotFound, typeof(ResponseFail404Example))]
        [SwaggerResponseExample((int)HttpStatusCode.BadRequest, typeof(ResponseFail400Example))]
        public async Task<IActionResult> DeleteTagById(Guid id)
        {
            return await SafeExecuteAsync(async () => await _tagService.DeleteTag(id), HttpMethod.Delete);
        }
    }
}